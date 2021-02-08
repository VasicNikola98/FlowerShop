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
        #region Singleton
        public static ProizvodService Instance
        {
            get
            {
                if (instance == null) instance = new ProizvodService();

                return instance;
            }
        }

        private static ProizvodService instance { get; set; }

        private ProizvodService()
        {

        }
        #endregion

        private IMongoCollection<Proizvod> _proizvodi;

        public List<Proizvod> VratiSveProizvode()
        {

            var db = SessionManager.GetMongoDB();

            _proizvodi = db.GetCollection<Proizvod>("Proizvodi");
            var proizvodi = _proizvodi.Find(p => true).ToList();

            return proizvodi;

        }
        public Proizvod VratiProizvod(string Id)
        {
            var db = SessionManager.GetMongoDB();
            _proizvodi = db.GetCollection<Proizvod>("Proizvodi");
            var proizvod = _proizvodi.Find<Proizvod>(x => true && x.Id == ObjectId.Parse(Id)).FirstOrDefault();
            return proizvod;
        }

        public void DodajProizvod(Proizvod proizvod)
        {
            var db = SessionManager.GetMongoDB();
            _proizvodi = db.GetCollection<Proizvod>("Proizvodi");
            _proizvodi.InsertOne(proizvod);

        }

        public void IzbrisiProizvod(string Id)
        {
            var db = SessionManager.GetMongoDB();
            _proizvodi = db.GetCollection<Proizvod>("Proizvodi");
            _proizvodi.DeleteOne(x => x.Id == ObjectId.Parse(Id));
        }

        public void IzmeniProizvod(string Id,Proizvod proizvod)
        {
            var db = SessionManager.GetMongoDB();
            _proizvodi = db.GetCollection<Proizvod>("Proizvodi");
            proizvod.Id = ObjectId.Parse(Id);
            _proizvodi.ReplaceOne(pr => pr.Id == ObjectId.Parse(Id), proizvod);
        }
    }
}
