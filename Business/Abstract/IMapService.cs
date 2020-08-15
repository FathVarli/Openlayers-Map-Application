using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface IMapService
    {
        IResult AddPoint(float x, float y, string no);
        IDataResult<List<Point>> GetAllPoint();
    }
}
