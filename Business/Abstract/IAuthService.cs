using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using Core.Models;

namespace Business.Abstract
{
   public interface IAuthService
   {
       IDataResult<User> Register(RegisterModel model);
       IDataResult<User> Login(LoginModel model);
       IResult Update(string userName,UserUpdateModel model);
   }
}
