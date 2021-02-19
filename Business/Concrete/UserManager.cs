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
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult Add(User entity)
        {
            
                _userDal.Add(entity);
                return new SuccessDataResult<List<User>>(Messages.UserAdded);
          
        }

        public IResult Delete(User entity)
        {
            _userDal.Delete(entity);
            return new SuccessDataResult<List<User>>(Messages.UserDeleted);
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll());
        }

        public IDataResult<User> GetById(int id)
        {
            return new SuccessDataResult<User>(_userDal.Get(p => p.UserId == id));
        }

        
        public IResult Update(User entity)
        {
            
                _userDal.Update(entity);
                return new SuccessDataResult<List<User>>(Messages.UserUpdated);
            
        }
    }
}
