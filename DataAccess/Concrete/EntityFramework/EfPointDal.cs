using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Context;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfPointDal : EfEntityRepositoryBase<Point, SqlContext>, IPointDal
    {
    }
}
