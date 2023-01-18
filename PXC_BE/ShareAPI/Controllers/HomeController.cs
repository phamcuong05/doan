using System;
using System.Web.Mvc;
using System.Net.Http;
using System.Data;
using Newtonsoft.Json;

namespace FTS.ShareAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
