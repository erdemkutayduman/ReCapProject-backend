using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }


        //[SecuredOperation("carImage.add,admin")]
        [ValidationAspect(typeof(CarImageValidator))]
        [CacheRemoveAspect("ICarImageService.Get")]
        public IResult Add(CarImage carImage, IFormFile file)
        {
            var result = BusinessRules.Run(CheckCarImageCount(carImage.CarId));

            if (result != null)
            {
                return result;
            }

            carImage.ImageDate = DateTime.Now;
            //carImage.ImagePath = FileHelper.AddFile(file);
            carImage.ImagePath = new FileHelper().Add(file, CreateNewPath(file));

            _carImageDal.Add(carImage);

            return new SuccessResult(Messages.CarImageAdded);
        }

        //[SecuredOperation("admin,carimage.delete")]
        [CacheRemoveAspect("ICarImageService.Get")]
        public IResult Delete(CarImage carImage)
        {
            var image = _carImageDal.Get(c => c.CarImageId == carImage.CarImageId);

            if (image == null)
            {
                return new ErrorResult(Messages.CarImageNotExist);
            }

            FileHelper.DeleteFile(image.ImagePath);

            _carImageDal.Delete(carImage);

            return new SuccessResult(Messages.CarImageDeleted);
        }


        //[SecuredOperation("admin,carimage.update")]
        [ValidationAspect(typeof(CarImageValidator))]
        [CacheRemoveAspect("ICarImageService.Get")]
        public IResult Update(CarImage carImage, IFormFile file)
        {
            var oldImage = _carImageDal.Get(c => c.CarImageId == carImage.CarImageId);

            if (oldImage == null)
            {
                return new ErrorResult(Messages.CarImageNotExist);
            }

            carImage.ImageDate = DateTime.Now;
            //carImage.ImagePath = FileHelper.UpdateFile(file, oldImage.ImagePath);
            carImage.ImagePath = new FileHelper().Update(oldImage.ImagePath, file, CreateNewPath(file));

            _carImageDal.Update(carImage);

            return new SuccessResult(Messages.CarImageUpdated);
        }

        [CacheAspect]
        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(), Messages.CarImagesListed);
        }

        [CacheAspect]
        public IDataResult<List<CarImage>> GetImagesByCarId(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId);

            IfCarImageOfCarNotExistsAddDefault(ref result);

            return new SuccessDataResult<List<CarImage>>(result);
        }

        public IDataResult<CarImage> GetById(int id)
        {
            var result = _carImageDal.Get(c => c.CarImageId == id);

            IfCarImageOfCarNotExistsAddDefault(ref result);

            return new SuccessDataResult<CarImage>(_carImageDal.Get(p => p.CarImageId == id));
        }

        //business rules

        private IResult CheckCarImageCount(int carId)
        {
            if (_carImageDal.GetAll(ci => ci.CarId == carId).Count >= 5)
            {
                return new ErrorResult(Messages.CarImageAddingLimit);
            }
            return new SuccessResult();
        }

        private void IfCarImageOfCarNotExistsAddDefault(ref List<CarImage> result)
        {
            if (!result.Any()) result.Add(CreateDefaultCarImage());
        }

        private void IfCarImageOfCarNotExistsAddDefault(ref CarImage result)
        {
            if (result == null) result = CreateDefaultCarImage();
        }

        private CarImage CreateDefaultCarImage()
        {
            var defaultCarImage = new CarImage
            {
                ImagePath = $@"{Environment.CurrentDirectory}\wwwroot\Uploads\34c3aade-ecae-4c3d-9708-8fc1ad2a0711_2_28_2021.jpg",
                ImageDate = DateTime.Now
            };

            return defaultCarImage;
        }

      
        private string CreateNewPath(IFormFile file)
        {
            var fileInfo = new FileInfo(file.FileName);
            var newPath =
                $@"{Environment.CurrentDirectory}\Public\Images\CarImage\Upload\{Guid.NewGuid()}_{DateTime.Now.Month}_{DateTime.Now.Day}_{DateTime.Now.Year}{fileInfo.Extension}";

            return newPath;
        }

   

    }
}

