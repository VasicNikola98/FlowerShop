using FlowerShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;


namespace FlowerShop.Services
{
    public class ProizvodService
    {
        private IMongoCollection<Proizvod> _proizvodi;
        public List<Proizvod> VratiSveProizvode()
        {

            var db = SessionManager.GetMongoDB();

            _proizvodi = db.GetCollection<Proizvod>("Proizvodi");
            var proizvodi = _proizvodi.Find(p => true).ToList();

            return proizvodi;

        }
    }
}
