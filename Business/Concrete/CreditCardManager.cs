using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CreditCardManager : ICreditCardService
    {

        ICreditCardDal _creditCardDal;

        public CreditCardManager(ICreditCardDal creditCardDal)
        {
            _creditCardDal = creditCardDal;
        }


        //[SecuredOperation("admin,creditcard.add")]
        [ValidationAspect(typeof(CreditCardValidator))]
        [CacheRemoveAspect("ICreditCardService.Get")]
        public IResult Add(CreditCard creditCard)
        {
            var result = BusinessRules.Run(CheckCardIsExists(creditCard.UserId, creditCard.CardNumber));

            if (result != null)
            {
                return result;
            }

            _creditCardDal.Add(creditCard);

            return new SuccessResult(Messages.CardAdded);
        }


        //[SecuredOperation("admin,creditcard.delete")]
        [CacheRemoveAspect("ICreditCardService.Get")]
        public IResult Delete(CreditCard creditCard)
        {
            _creditCardDal.Delete(creditCard);

            return new SuccessResult(Messages.CardDeleted);
        }


        //[SecuredOperation("admin,creditcard.update")]
        [ValidationAspect(typeof(CreditCardValidator))]
        [CacheRemoveAspect("ICreditCardService.Get")]
        public IResult Update(CreditCard creditCard)
        {
            _creditCardDal.Update(creditCard);

            return new SuccessResult(Messages.CardUpdated);
        }

        IDataResult<CreditCard> IBaseService<CreditCard>.GetById(int id)
        {
            return new SuccessDataResult<CreditCard>(_creditCardDal.Get(c => c.UserId == id));
        }

        public IDataResult<List<CreditCard>> GetAll()
        {
            return new SuccessDataResult<List<CreditCard>>(_creditCardDal.GetAll(), Messages.CardsListed);
        }


        //[CacheAspect]
        //[PerformanceAspect(5)]

        // Business Rules Methods
        private IResult CheckCardIsExists(int userId, string cardNumber)
        {
            var result = _creditCardDal.Get(c => c.CardNumber == cardNumber
                                            && c.UserId == userId);

            if (result != null)
            {
                return new ErrorResult(Messages.CardExists);
            }
            return new SuccessResult();
        }   
    }
}
