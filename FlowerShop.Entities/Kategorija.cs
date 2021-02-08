using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FlowerShop.Entities
{
    public class Kategorija
    {
        public ObjectId Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public bool Istaknuta { get; set; }

        public List<MongoDBRef> Proizvodi { get; set; }

        public Kategorija()
        {
            Proizvodi = new List<MongoDBRef>();
        }

    }
}
