using FlowerShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlowerShop.ViewModels
{
    public class KorpaViewModel
    {
        public List<Korpa> Korpe { get; set; }
        public List<Proizvod> MojiProizvodi { get; set; }
    }
}