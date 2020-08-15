using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Entities.Concrete;
using Core.Models;
using Core.Utilities.Results;
using Core.Utilities.Security;
using DataAccess.Abstract;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserDal _userDal;
        private IUserService _userService;

        public AuthManager(IUserDal userDal, IUserService userService)
        {
            _userDal = userDal;
            _userService = userService;
        }

        public IDataResult<User> Register(RegisterModel model)
        {
            var checkUserName = _userService.GetByUserName(model.UserName);
            if (checkUserName != null)
            {
                return new ErrorDataResult<User>("Bu kullanici adi kullanılıyor!");
            }
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);
            var user = new User
            {
                UserName = model.UserName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            _userDal.Add(user);
            return new SuccessDataResult<User>(user);
        }

        public IDataResult<User> Login(LoginModel model)
        {
            var userToCheck = _userService.GetByUserName(model.UserName);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>("User not found!");
            }

            if (!HashingHelper.VerifyPasswordHash(model.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>("Kullanici adi veya sifre hatali!");
            }

            return new SuccessDataResult<User>(userToCheck); ;
        }

        public IResult Update(string userName, UserUpdateModel model)
        {
            var user = _userService.GetByUserName(userName);
            if (user == null)
            {
                return new ErrorResult("Kullanici bulunamadi");
            }
            else
            {
                if (!String.IsNullOrEmpty(model.UserName))
                {
                    user.UserName = model.UserName;
                }
                if (!String.IsNullOrEmpty(model.NameSurname))
                {
                    user.NameSurName = model.NameSurname;
                }

                if (!String.IsNullOrEmpty(model.Password))
                {
                    byte[] passwordHash, passwordSalt;
                    HashingHelper.CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);

                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                }

                _userDal.Update(user);
                return new SuccessResult("Kullanici başarıyla güncellendi!");
            }
        }
    }
}
