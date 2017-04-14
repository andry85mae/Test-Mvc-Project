using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Magneti_Marelli_Test.Controllers
{
    public class HomeController : Controller
    {
        [HandleError]
        public ActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HandleError]
        public ActionResult About()
        {
            try
            {
                ViewBag.Message = "Your application description page.";

                return View();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HandleError]
        public ActionResult Contact()
        {
            try
            {
                ViewBag.Message = "Your contact page.";

                return View();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}