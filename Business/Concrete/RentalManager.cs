using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;
        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental rental)
        {

            IResult results = BusinessRules.Run(CheckIfCarInUse(rental.CarId));
            if (results != null)
            {
                return results;
            }
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.RentalAdded);
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.RentalDeleted);
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        public IDataResult<Rental> GetById(int id)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(I => I.Id == id));
        }

        public IDataResult<List<RentalDetailDto>> GetRentalDetails(Expression<Func<Rental, bool>> filter = null)
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails(filter), Messages.RentalsListed);
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.RentalUpdated);
        }

        public IResult CheckReturnDate(int carId)
        {
            var result = _rentalDal.GetRentalDetails(p => p.CarId == carId && p.ReturnDate == null);
            if (result.Count > 0)
            {
                return new ErrorResult(Messages.RentalNameInvalid);
            }
            return new SuccessResult(Messages.RentalAdded);
        }

        
        public IResult UpdateReturnDate(int carId)
        {
            var result = _rentalDal.GetAll(p => p.CarId == carId);
            var updatedRental = result.LastOrDefault();
            if (updatedRental.ReturnDate != null)
            {
                return new ErrorResult();
            }
            updatedRental.ReturnDate = DateTime.Now;
            _rentalDal.Update(updatedRental);
            return new SuccessResult();
        }

        private IResult CheckIfCarInUse(int carId)
        {
            var result = _rentalDal.Get(p => p.CarId == carId && p.ReturnDate == null);
            if (result != null)
            {
                return new ErrorResult(Messages.RentalBusy);
            }
            return new SuccessResult();

        }

        private IResult CheckIfDelete(int Id)
        {
            var result = _rentalDal.Get(p => p.Id == Id);
            if (result == null)
            {
                return new ErrorResult(Messages.RentalRecordsInvalid);
            }
            if (result.ReturnDate == null)
            {
                return new ErrorResult(Messages.RentalBusy);
            }
            return new SuccessResult();
        }

        private IResult CheckIfDeliver(int Id)
        {
            var result = _rentalDal.Get(p => p.Id == Id);
            if (result.ReturnDate != null)
            {
                return new ErrorResult(Messages.RentalRecordsInvalid);
            }
            result.ReturnDate = DateTime.Now.Date;
            Update(result);
            return new SuccessResult();
        }
    }
}
