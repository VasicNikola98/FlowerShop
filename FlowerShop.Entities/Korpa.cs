using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Entities
{
    public class Korpa
    {
        public ObjectId Id { get; set; }
        public string HashUserId { get; set; }
        public int Kolicina { get; set; }
        public MongoDBRef Proizvod { get; set; }

    }
}
