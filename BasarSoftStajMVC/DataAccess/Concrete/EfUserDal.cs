using BasarSoftStajMVC.DataBase;
using BasarSoftStajMVC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasarSoftStajMVC.DataAccess.Concrete
{
    public class EfUserDal : EfEntityRepositoryBase<User, SqlContext>
    {
    }
}