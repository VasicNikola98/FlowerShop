using FlowerShop.Entities;
using FlowerShop.Services;
using FlowerShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlowerShop.Controllers
{
    public class KategorijaController : Controller
    {
        // GET: Kategorija
        public ActionResult Index()
        {
            KategorijaViewListingModel model = new KategorijaViewListingModel();
            model.Kategorije = KategorijaService.Instance.VratiSveKategorije();

            return View(model);
        }

        [HttpGet]
        public ActionResult DodajKategoriju()
        {
            return View();
        }

        [HttpPost]
        public JsonResult DodajKategoriju(Kategorija kategorija)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (kategorija != null)
            {
                KategorijaService.Instance.DodajKategoriju(kategorija);
                result.Data = new { Success = true };
            }
            else
            {
                result.Data = new { Success = false };
            }
            return result;

        }

        [HttpPost]
        public JsonResult DodajUKategoriju(string IdKategorije,string IdProizvoda)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (!string.IsNullOrEmpty(IdKategorije) && !string.IsNullOrEmpty(IdProizvoda))
            {
                KategorijaService.Instance.DodajUKategoriju(IdKategorije, IdProizvoda);
                result.Data = new { Success = true };
            }
            else
            {
                result.Data = new { Success = false };
            }
            return result;
        }

        [HttpGet]
        public ActionResult ProizvodiPoKategoriji(string IdKategorije)
        {
            KategorijaService.Instance.VratiProizvodePoKategoriji(IdKategorije);
            return View();
        }

        public JsonResult IzbrisiKategoriju(string IdKategorije)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (!string.IsNullOrEmpty(IdKategorije))
            {
                KategorijaService.Instance.IzbrisiKategoriju(IdKategorije);
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