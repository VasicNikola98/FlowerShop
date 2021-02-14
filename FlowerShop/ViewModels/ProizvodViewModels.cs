using FlowerShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlowerShop.ViewModels
{
    public class ProizvodViewModel
    {
        public List<Proizvod> Proizvodi { get; set; }
    }

    public class NoviProizvodViewModel
    {
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public string ImageUrl { get; set; }
        public float Cena { get; set; }
        public string Kategorija { get; set; }

    }
}