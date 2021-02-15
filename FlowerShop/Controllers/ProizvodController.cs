using FlowerShop.Entities;
using FlowerShop.Services;
using FlowerShop.ViewModels;
using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlowerShop.Controllers
{
    public class ProizvodController : Controller
    {
        // GET: Proizvod
        public ActionResult Index(string IdKategorije, string searchTerm)
        {
            ProizvodViewModel model = new ProizvodViewModel();
           
            if (!string.IsNullOrEmpty(IdKategorije))
            {
                model.Proizvodi = ProizvodService.Instance.VratiSveProizvode(IdKategorije);
                
            }
            else if(!string.IsNullOrEmpty(searchTerm))
            {
                model.Proizvodi = ProizvodService.Instance.VratiSveProizvodeSearch(searchTerm);
            }
            else
            {
                model.Proizvodi = ProizvodService.Instance.VratiSveProizvode();
            }
            return View(model);
        }
       
        [HttpGet]
        public ActionResult DodajProizvod()
        {
            KategorijaViewListingModel model = new KategorijaViewListingModel();
            model.Kategorije = KategorijaService.Instance.VratiSveKategorije();
            return View(model);
        }

        [HttpPost]
        public JsonResult DodajProizvod(NoviProizvodViewModel proizvod)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (proizvod != null)
            {
                Proizvod noviProizvod = new Proizvod();
                noviProizvod.Naziv = proizvod.Naziv;
                noviProizvod.Opis = proizvod.Opis;
                noviProizvod.ImageUrl = proizvod.ImageUrl;
                noviProizvod.Cena = proizvod.Cena;
                noviProizvod.Kategorija = new MongoDBRef("Kategorija", proizvod.Kategorija);
                ProizvodService.Instance.DodajProizvod(noviProizvod);
                result.Data = new { Success = true };
            }
            else
            { 
                result.Data = new { Success = false };
            }
            return result;

        }


        [HttpPost]
        public JsonResult IzmeniProizvod(string Id, Proizvod proizvod)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (proizvod != null)
            {
                ProizvodService.Instance.IzmeniProizvod(Id, proizvod);
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

        public JsonResult UploadImage()
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            try
            {
                var file = Request.Files[0];

                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

                var path = Path.Combine(Server.MapPath("~/content/images/"), fileName);

                file.SaveAs(path);

                result.Data = new { Success = true, ImageUrl = string.Format("/content/images/{0}", fileName) };

            }
            catch (Exception ex)
            {
                result.Data = new { Success = false, Message = ex.Message };
            }
            return result;
        }
    }
}