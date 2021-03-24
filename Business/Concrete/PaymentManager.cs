using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class PaymentManager : IPaymentService
    {
        IPaymentDal _paymentDal;


        public PaymentManager(IPaymentDal paymentDal)
        {
            _paymentDal = paymentDal;
        }


        [ValidationAspect(typeof(PaymentValidator))]
        public IResult Add(Payment payment)
        {
            _paymentDal.Add(payment);

            return new SuccessResult(Messages.PaymentAdded);
        }

        public IResult Delete(Payment entity)
        {
            _paymentDal.Delete(entity);

            return new SuccessResult(Messages.PaymentDeleted);
        }

        public IDataResult<List<Payment>> GetAll()
        {
            return new SuccessDataResult<List<Payment>>(_paymentDal.GetAll(), Messages.PaymentsListed);
        }

        public IDataResult<Payment> GetById(int id)
        {
            return new SuccessDataResult<Payment>(_paymentDal.Get(p => p.PaymentId == id));
        }

        public IResult Update(Payment entity)
        {
            _paymentDal.Update(entity);

            return new SuccessResult(Messages.PaymentUpdated);
        }
    }
}
