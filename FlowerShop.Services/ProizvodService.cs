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
        private IMongoCollection<Kategorija> _kategorije;

        public List<Proizvod> VratiSveProizvode()
        {

            var db = SessionManager.GetMongoDB();

            _proizvodi = db.GetCollection<Proizvod>("Proizvodi");
            var proizvodi = _proizvodi.Find(p => true).ToList();

            return proizvodi;

        }

        public List<Proizvod> VratiSveProizvode(string IdKategorije)
        {

            var db = SessionManager.GetMongoDB();

            _proizvodi = db.GetCollection<Proizvod>("Proizvodi");


            var r = new MongoDBRef("Kategorija",IdKategorije);
            var proizvodi = _proizvodi.Find(p => true && p.Kategorija.Id == r.Id).ToList();

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
            _kategorije = db.GetCollection<Kategorija>("Kategorije");
            var kategorija = _kategorije.Find<Kategorija>(x => true && x.Id == ObjectId.Parse(proizvod.Kategorija.Id.AsString)).FirstOrDefault();
      
            _proizvodi.InsertOne(proizvod);

            var p = _proizvodi.Find<Proizvod>(x => true && x.Naziv.Contains(proizvod.Naziv) && x.Cena == proizvod.Cena && x.ImageUrl.Contains(proizvod.ImageUrl)).FirstOrDefault();

            kategorija.Proizvodi.Add(new MongoDBRef("Proizvodi", p.Id));
            _kategorije.ReplaceOne(x => x.Id == kategorija.Id, kategorija);

        }

        public void IzbrisiProizvod(string Id)
        {
            var db = SessionManager.GetMongoDB();
            _proizvodi = db.GetCollection<Proizvod>("Proizvodi");
            var proizvod = _proizvodi.Find<Proizvod>(x => true && x.Id == ObjectId.Parse(Id)).FirstOrDefault();
            _kategorije= db.GetCollection<Kategorija>("Kategorije");
            var kategorija = _kategorije.Find<Kategorija>(x => true && x.Id == ObjectId.Parse(proizvod.Kategorija.Id.ToString())).FirstOrDefault();
            for(var i = 0; i< kategorija.Proizvodi.Count; i++)
            {
                if (kategorija.Proizvodi[i].Id == proizvod.Id)
                {
                    kategorija.Proizvodi.Remove(kategorija.Proizvodi[i]);
                }
            }
            _kategorije.ReplaceOne(x => true && x.Id == kategorija.Id, kategorija);
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
