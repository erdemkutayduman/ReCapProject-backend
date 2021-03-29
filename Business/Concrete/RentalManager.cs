using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
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

        [SecuredOperation("rental.add,admin")]
        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental rental)
        {

            IResult results = BusinessRules.Run(CarAvailabilityCheck(rental),
                                                FindeksScoreAvailabilityCheck(rental));

            if (results != null)
            {
                return results;
            }

            _rentalDal.Add(rental);
            return new SuccessResult(Messages.RentalAdded);
        }

        [SecuredOperation("rental.delete,admin")]
        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.RentalDeleted);
        }

        [CacheAspect]
        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<Rental> GetById(int id)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(I => I.Id == id));
        }

        public IDataResult<List<RentalDetailDto>> GetRentalDetails(Expression<Func<Rental, bool>> filter = null)
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails(filter), Messages.RentalsListed);
        }

        [SecuredOperation("rental.update,admin")]
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

        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<Rental> GetIdByRentalDetails(int carId, int customerId, DateTime rentDate, DateTime returnDate)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.CarId == carId
                                                                && r.CustomerId == customerId
                                                                && r.RentDate == rentDate
                                                                && r.ReturnDate == returnDate));
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


        //Business Rules


        private IResult CarAvailabilityCheck(Rental rental)
        {
            var overlappingDateList = _rentalDal.GetRentalDetails(r => r.CarId == rental.CarId
                                                                  && r.RentDate < rental.ReturnDate
                                                                  && r.ReturnDate > rental.RentDate);

            if (overlappingDateList.Count() == 0)
            {
                return new SuccessResult();
            }
            else
            {
                return new ErrorResult(Messages.RentalBusy);
            }
        }

        private IResult FindeksScoreAvailabilityCheck(Rental rental)
        {
            var result = _rentalDal.GetCreditScores(rental.CarId, rental.CustomerId);

            if (result.CarMinCarCreditScore <= result.CustomerCarCreditScore)
            {
                return new SuccessResult();
            }
            else
            {
                return new ErrorResult(Messages.FindeksScoreIsNotEnough);
            }
        }

    }
}
