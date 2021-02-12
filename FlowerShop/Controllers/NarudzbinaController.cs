﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlowerShop.Entities;
using FlowerShop.Services;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FlowerShop.Controllers
{
    public class NarudzbinaController : Controller
    {
        // GET: Narudzbina
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult KreirajNarudzbinu(Posiljka posiljka)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            var hashId = Request.Cookies.Get("cartItemHashedUserId");
            var mojaKorpa = KorpaService.Instance.VratiProizvodeByHashedID(hashId.Value);
            posiljka.DatumPorucivanja = DateTime.Now;
            posiljka.Proizvodi = mojaKorpa.Select(x => new MongoDBRef("Proizvodi", x.Proizvod.Id)).ToList();
            NarudzbinaService.Instance.KreirajNarudzbinu(posiljka);
            KorpaService.Instance.IzbrisiKorpu(hashId.Value);
            result.Data = new { Success = true };
            return result;
        }
    }
}