using FlowerShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlowerShop.ViewModels
{
    public class NarudzbinaViewListingModel
    {
        public List<Posiljka> Posiljke { get; set; }
    }

    public class NarudzbinaViewModel
    {
        public Posiljka Posiljka { get; set; }
     
    }

}