using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Entities.Concrete;
using DataAccess.Abstract;

namespace Business.Concrete
{
    public class UserManager:IUserService
    {
        private IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public User GetByUserName(string userName)
        {
            var user = _userDal.Get(u => u.UserName == userName);
            return user ?? null;
        }


        public User GetById(int id)
        {
            var user = _userDal.Get(u => u.Id == id);
            return user ?? null;
        }
    }
}
