using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);        
        IDataResult<AccessToken> CreateAccessToken(User user);
        IResult UserExists(string email);
        IResult ChangePassword(UserForPasswordDto userForPasswordDto);

    }
}
