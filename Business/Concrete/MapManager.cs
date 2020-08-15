using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;

namespace Business.Concrete
{
    public class MapManager : IMapService
    {
        private IPointDal _pointDal;

        public MapManager(IPointDal pointDal)
        {
            _pointDal = pointDal;
        }

        public IResult AddPoint(float x, float y, string no)
        {
            var point = new Point
            {
                PointNo = no,
                XCoordinate = x,
                YCoordinate = y,
            };
            _pointDal.Add(point);
            return new SuccessResult("Point ekleme başarılı!");

        }

        public IDataResult<List<Point>> GetAllPoint()
        {
            var pointList = _pointDal.GetList();
            return new SuccessDataResult<List<Point>>(pointList);
        }
    }
}
