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
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IResult Add(User entity)
        {
            if (entity.FirstName.Length >= 2)
            {
                _userDal.Add(entity);
                return new SuccessDataResult<List<User>>(Messages.UserAdded);
            }
            else
            {
                return new ErrorDataResult<List<User>>(Messages.UserNameInvalid);
            }
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
            if (entity.FirstName.Length >= 2)
            {
                _userDal.Update(entity);
                return new SuccessDataResult<List<User>>(Messages.UserUpdated);
            }
            else
            {
                return new ErrorDataResult<List<User>>(Messages.UserNameInvalid);
            }
        }
    }
}
