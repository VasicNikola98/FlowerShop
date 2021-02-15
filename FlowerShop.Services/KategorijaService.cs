using FlowerShop.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Services
{
    public class KategorijaService
    {
        #region Singleton
        public static KategorijaService Instance
        {
            get
            {
                if (instance == null) instance = new KategorijaService();

                return instance;
            }
        }

        private static KategorijaService instance { get; set; }

        private KategorijaService()
        {

        }
        #endregion

        private IMongoCollection<Kategorija> _kategorije;

        public void DodajKategoriju(Kategorija kategorija)
        {
            var db = SessionManager.GetMongoDB();
            _kategorije = db.GetCollection<Kategorija>("Kategorije");
            _kategorije.InsertOne(kategorija);
        }

        public void DodajUKategoriju(string IdKategorije, string IdProizvoda)
        {
            var db = SessionManager.GetMongoDB();
            _kategorije = db.GetCollection<Kategorija>("Kategorije");
            var proizvod = ProizvodService.Instance.VratiProizvod(IdProizvoda);
            var kategorija = _kategorije.Find<Kategorija>(k => true && k.Id == ObjectId.Parse(IdKategorije)).FirstOrDefault();

            kategorija.Proizvodi.Add(new MongoDBRef("Proizvodi", proizvod.Id));
            proizvod.Kategorija = new MongoDBRef("Kategorija", kategorija.Id);
            ProizvodService.Instance.IzmeniProizvod(IdProizvoda, proizvod);
            _kategorije.ReplaceOne(x => x.Id == ObjectId.Parse(IdKategorije), kategorija);

        }

        public List<Proizvod> VratiProizvodePoKategoriji(string IdKategorije)
        {
            var db = SessionManager.GetMongoDB();
            List<Proizvod> proizvodi = new List<Proizvod>();

            _kategorije = db.GetCollection<Kategorija>("Kategorije");
            var kategorija = _kategorije.Find<Kategorija>(x => x.Id == ObjectId.Parse(IdKategorije)).FirstOrDefault();
            foreach(var i in kategorija.Proizvodi)
            {
                Proizvod proizvod = ProizvodService.Instance.VratiProizvod(i.Id.ToString());
                proizvodi.Add(proizvod);
            }

            return proizvodi;
        }

        public void IzbrisiKategoriju(string IdKategorije)
        {
            var db = SessionManager.GetMongoDB();
            _kategorije = db.GetCollection<Kategorija>("Kategorije");
            var kategorija = _kategorije.Find<Kategorija>(x => x.Id == ObjectId.Parse(IdKategorije)).FirstOrDefault();
           
            foreach (var i in kategorija.Proizvodi)
            {
                ProizvodService.Instance.IzbrisiProizvod(i.Id.ToString());
                KorpaService.Instance.IzbrisiProizvodIzKorpe(i.Id.ToString());

            }

            _kategorije.DeleteOne(x => x.Id == ObjectId.Parse(IdKategorije));

        }

        public Kategorija VratiKategoriju(string Id)
        {
            var db = SessionManager.GetMongoDB();
            _kategorije = db.GetCollection<Kategorija>("Kategorije");
            return _kategorije.Find<Kategorija>(x => true && x.Id == ObjectId.Parse(Id)).FirstOrDefault();
        }

        public List<Kategorija> VratiSveKategorije()
        {
            var db = SessionManager.GetMongoDB();
            _kategorije = db.GetCollection<Kategorija>("Kategorije");
            return _kategorije.Find<Kategorija>(x => true).ToList();
        }
    }
}
