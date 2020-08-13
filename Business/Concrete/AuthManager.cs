using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Entities.Concrete;
using Core.Model;
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
            //var checkEmail = _userService.GetByEmail(model.EMail);
            //if (checkEmail != null)
            //{
            //    return new ErrorDataResult<User>("Bu mail adresi kullanılıyor!");
            //}
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Name = model.Name,
                EMail = model.EMail,
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
                return new ErrorDataResult<User>("Sifre yanlış");
            }

            return new SuccessDataResult<User>(userToCheck); ;
        }

        public IResult Update(int id, UserUpdateModel model)
        {
            var user = _userService.GetById(id);
            if (user == null)
            {
                return new ErrorResult("Kullanici bulunamadi");
            }
            else
            {
                if (!String.IsNullOrEmpty(model.Name))
                {
                    user.Name = model.Name;
                }

                if (!String.IsNullOrEmpty(model.Email))
                {
                    user.EMail = model.Email;
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
