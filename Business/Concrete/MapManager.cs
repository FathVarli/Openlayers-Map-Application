using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Globalization;
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
        private IPolygonDal _polygonDal;

        public MapManager(IPointDal pointDal, IPolygonDal polygonDal)
        {
            _pointDal = pointDal;
            _polygonDal = polygonDal;
        }

        public IResult AddPoint(float x, float y, string no)
        {
            var point = new Point
            {
                KapiNo= no,
                x = x,
                y = y,
            };
            _pointDal.Add(point);
            return new SuccessResult("Point ekleme başarılı!");

        }

        public IDataResult<List<Point>> GetAllPoint()
        {
            var pointList = _pointDal.GetList();
            return new SuccessDataResult<List<Point>>(pointList);
        }

        public IDataResult<List<string[][]>> GetAllPolygon()
        {
            int j = 0;
            var polygonAllList = new List<string[][]>();
            string[] arrCoordinate;
            var polygonList = _polygonDal.GetList();
            foreach (var polygon in polygonList)
            {
                if (polygon.Coordinates.PointCount != null)
                {
                    string[][] coordinates = new string[(int)polygon.Coordinates.PointCount][];
                    var polygonString = Convert.ToString(polygon.Coordinates.ProviderValue);
                    var coord = polygonString.Substring(10, polygonString.Length - 12);
                    arrCoordinate = coord.Split(',');
                    for (int i = 0; i < coordinates.Length; i++)
                    {
                        for (; j < arrCoordinate.Length;)
                        {
                            var x = arrCoordinate[j].Trim().Split(' ');
                            coordinates[i] = new string[2] { x[0], x[1] };
                            break;
                        }

                        j++;
                    }
                    polygonAllList.Add(coordinates);
                    j = 0;
                }
            }
            return new SuccessDataResult<List<string[][]>>(polygonAllList);
        }

        public IResult AddPolygon(string[][] coordinates, string polygonName)
        {
            string coord = "";
            foreach (var coordinate in coordinates)
            {
                coord += $"{coordinate[0]} {coordinate[1]},";
            }

            var coord2 = coord.Substring(0, coord.Length - 1);

            var polygon = new Polygon
            {
                PolygonName = polygonName,
                Coordinates = DbGeometry.FromText($"POLYGON(({coord2}))")
                
            };
            _polygonDal.Add(polygon);
            return new SuccessResult("Başarıyla eklendi!");
        }
    }
}
