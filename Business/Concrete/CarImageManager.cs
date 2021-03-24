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
        [CacheRemoveAspect("ICarImageService.Get")]
        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Add(CarImage carImage)
        {
            var result = BusinessRules.Run(CheckIfCarImagesExceed(carImage.CarId));
            if (result != null)
            {
                return new ErrorResult(result.Message);
            }
            _carImageDal.Add(carImage);
            return new SuccessResult(Messages.CarImageAdded);
        }

        //[SecuredOperation("carImage.delete,admin")]
        [CacheRemoveAspect("ICarImageService.Get")]
        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Delete(CarImage carImage)
        {
            var result = BusinessRules.Run(CheckIfDataExists());
            if (result != null)
            {
                return new ErrorResult(result.Message);
            }
            _carImageDal.Delete(carImage);
            return new SuccessResult(Messages.CarImageDeleted);
        }

        //[SecuredOperation("carImage.update,admin")]
        [CacheRemoveAspect("ICarImageService.Get")]
        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Update(CarImage carImage)
        {
            _carImageDal.Update(carImage);
            return new SuccessResult(Messages.CarImageUpdated);
        }

        [ValidationAspect(typeof(CarImageValidator))]
        public IDataResult<CarImage> Get(int id)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(p => p.CarImageId == id));
        }

        [CacheAspect]
        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }
        [ValidationAspect(typeof(CarImageValidator))]
        public IDataResult<List<CarImage>> GetImagesByCarId(int id)
        {
            return new SuccessDataResult<List<CarImage>>(CheckIfCarImageNull(id));
        }

        //business rules
        
        private List<CarImage> CheckIfCarImageNull(int id)
        {
            string path = @"\Images\logo.jpg";
            var result = _carImageDal.GetAll(c => c.CarId == id).Any();
            if (!result)
            {
                return new List<CarImage> { new CarImage { CarId = id, ImagePath = path, ImageDate = DateTime.Now } };
            }
            return _carImageDal.GetAll(p => p.CarId == id);

        }

        public IDataResult<List<CarImage>> GetAllByCarId(int carId)
        {
            var result = BusinessRules.Run(CheckIfCarImageExists(carId));
            if (result != null)
            {
                return new ErrorDataResult<List<CarImage>>(new List<CarImage> {
                    new CarImage {CarImageId=404, ImageName="DefaultImage", CarId = carId, ImageDate = DateTime.Now, ImagePath = FilePaths.ImageDefaultPath}}, result.Message);
            }
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(image => image.CarId == carId), Messages.CarImagesListed);
        }

        private IResult CheckIfCarImageExists(int carId)
        {
            var result = _carImageDal.GetAll(image => image.CarId == carId).Any();
            if (!result)
            {
                return new ErrorResult(Messages.CarImageNotExist);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCarImagesExceed(int carId)
        {
            var result = _carImageDal.GetAll(image => image.CarId == carId).Count;
            if (result >= 5)
            {
                return new ErrorResult(Messages.CarImageAddingLimit);
            }
            return new SuccessResult();
        }

        private IResult CheckIfDataExists()
        {
            var result = _carImageDal.GetAll().Any();
            if (!result)
            {
                return new ErrorResult(Messages.CarImageNotExist);
            }
            return new SuccessResult();
        }

        public IDataResult<CarImage> GetById(int id)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(p => p.CarImageId == id));
        }
    }
}
