using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Driver;
using MongoDB.Bson;

namespace FlowerShop.Entities
{
    public class Proizvod
    {
        public ObjectId Id { get; set; }
        public string Naziv { get; set; }
        //public string Opis { get; set; }
        //public string ImageUrl { get; set; }
        //public float Cena { get; set; }
    }
}
