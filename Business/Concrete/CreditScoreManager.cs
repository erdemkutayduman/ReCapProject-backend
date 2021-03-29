using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CreditScoreManager : ICreditScoreService
    {
        ICreditScoreDal _creditScoreDal;

        public CreditScoreManager(ICreditScoreDal creditScoreDal)
        {
            _creditScoreDal = creditScoreDal;
        }
        public IResult Add(CreditScore entity)
        {
            _creditScoreDal.Add(entity);
            return new SuccessResult(Messages.CreditScoreAdded);
        }

        public IResult Delete(CreditScore entity)
        {
            _creditScoreDal.Delete(entity);
            return new SuccessResult(Messages.CreditScoreDeleted);
        }

        public IDataResult<List<CreditScore>> GetAll()
        {
            return new SuccessDataResult<List<CreditScore>>(_creditScoreDal.GetAll());
        }

        public IDataResult<CreditScore> GetById(int id)
        {
            return new SuccessDataResult<CreditScore>(_creditScoreDal.Get(c => c.CreditScoreId == id));
        }

        public IResult Update(CreditScore entity)
        {
            _creditScoreDal.Update(entity);
            return new SuccessResult(Messages.CreditScoreUpdated);
        }
    }
}
