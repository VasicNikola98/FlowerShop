using FlowerShop.Entities;
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
            ProizvodService.Instance.VratiSveProizvode();
            return View();
        }

        [HttpPost]
        public JsonResult DodajProizvod(Proizvod proizvod)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (proizvod != null)
            {
                ProizvodService.Instance.DodajProizvod(proizvod);
                result.Data = new { Success = true };
            }
            else
            {
                result.Data = new { Success = false };
            }
            return result;

        }

        [HttpPost]
        public JsonResult IzbrisiProizvod(string Id)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (!string.IsNullOrEmpty(Id))
            {
                ProizvodService.Instance.IzbrisiProizvod(Id);
                result.Data = new { Success = true };
            }
            else
            {
                result.Data = new { Success = false };
            }
            return result;
        }

        [HttpPost]
        public JsonResult IzmeniProizvod(string Id,Proizvod proizvod)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (proizvod != null)
            {
                ProizvodService.Instance.IzmeniProizvod(Id,proizvod);
                result.Data = new { Success = true };
            }
            else
            {
                result.Data = new { Success = false };
            }
            return result;
        }
    }
}