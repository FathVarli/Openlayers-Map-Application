using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCWebUI.Controllers
{
    public class MapController : Controller
    {
        [Authorize]
        public ActionResult GetMap()
        {
            return View();
        }



    }
}