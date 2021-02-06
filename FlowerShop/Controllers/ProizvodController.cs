using FlowerShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlowerShop.Controllers
{
    public class ProizvodController : Controller
    {
        // GET: Proizvod
        public ActionResult Index()
        {
            ProizvodService p = new ProizvodService();
            p.VratiSveProizvode();
            return View();
        }
    }
}