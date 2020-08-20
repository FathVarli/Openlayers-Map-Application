using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business.Abstract;

namespace MVCWebUI.Controllers
{
    public class MapController : Controller
    {
        private IMapService _mapService;

        public MapController(IMapService mapService)
        {
            _mapService = mapService;
        }

        [Authorize]
        public ActionResult GetMap()
        {
            return View();
        }
        public JsonResult SavePoint()
        {
            return Json("");
        }

        [HttpPost]
        public JsonResult SavePoint(float x, float y, string no)
        {
            if (ModelState.IsValid)
            {
                var result = _mapService.AddPoint(x, y, no);
                return Json(result.Message);
            }
            return Json("");
        }
        public JsonResult SavePolygon()
        {
            return Json("");
        }

        [HttpPost]
        public JsonResult SavePolygon(string[][] coordinatesArr,string no)
        {
            if (ModelState.IsValid)
            {
                var result = _mapService.AddPolygon(coordinatesArr, no);
                return Json(result.Message);
            }
            return Json("");
        }
        [HttpGet]
        public JsonResult ListPoint()
        {
            var points = _mapService.GetAllPoint();
            return Json(points.Data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListPolygon()
        {
            var result = _mapService.GetAllPolygon();
            return Json(result.Data, JsonRequestBehavior.AllowGet);
        }
        

    }
}