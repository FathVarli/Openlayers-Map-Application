﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using DataAccess.Concrete;

namespace DataAccess.Abstract
{
    public interface IPolygonDal:IEntityRepository<Polygon>
    {
    }
}
