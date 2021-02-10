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
    public class KorpaController : Controller
    {
        // GET: Korpa
        public ActionResult Index()
        {
            KorpaViewModel model = new KorpaViewModel();
            var hash = Request.Cookies.Get("cartItemHashedUserId");
            if (hash != null && !string.IsNullOrEmpty(hash.Value))
            {
               model.Korpe = KorpaService.Instance.VratiProizvodeByHashedID(hash.Value);
               model.MojiProizvodi = new List<Proizvod>();

               foreach(var item in model.Korpe)
                {
                    var s = item.Proizvod.Id.AsBsonValue;
                   
                    model.MojiProizvodi.Add(ProizvodService.Instance.VratiProizvod(s.ToString()));
                }
            }
           
            return View(model);
        }

        public JsonResult DodajUKorpu(string Kolicina, string IdProizvoda)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            var hash = Request.Cookies.Get("cartItemHashedUserId");
            if (!string.IsNullOrEmpty(IdProizvoda) && !string.IsNullOrEmpty(Kolicina) && !string.IsNullOrEmpty(hash.Value))
            {
                Korpa korpa = new Korpa();
                korpa.HashUserId = hash.Value;
                korpa.Kolicina = int.Parse(Kolicina);
                KorpaService.Instance.DodajUKorpu(korpa, IdProizvoda);
                result.Data = new { Success = true };
            }
            else
            {
                result.Data = new { Success = false };
            }
            return result;
        }

        public JsonResult BrojProizvodaUKorpi()
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            var hash = Request.Cookies.Get("cartItemHashedUserId");
            var brojacProizvoda = 0;

            if(hash != null && !string.IsNullOrEmpty(hash.Value))
            {
                var mojaKorpa = KorpaService.Instance.VratiProizvodeByHashedID(hash.Value);
                foreach(var item in mojaKorpa)
                {
                    brojacProizvoda += item.Kolicina;
                }
            }
            else
            {
                brojacProizvoda = 0;
            }

            result = new JsonResult { Data = brojacProizvoda, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            return result;
        }

    }
}