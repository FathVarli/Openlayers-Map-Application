using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;

namespace Business.Abstract
{
    public interface IUserService
    {
        User GetByUserName(string userName);
        User GetByEmail(string email);
        User GetById(int id);
    }
}
