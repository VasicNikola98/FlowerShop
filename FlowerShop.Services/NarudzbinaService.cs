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
    public class NarudzbinaService
    {
        #region Singleton
        public static NarudzbinaService Instance
        {
            get
            {
                if (instance == null) instance = new NarudzbinaService();

                return instance;
            }
        }

        private static NarudzbinaService instance { get; set; }

        private NarudzbinaService()
        {

        }
        #endregion

        private IMongoCollection<Posiljka> _posiljke;



        public void KreirajNarudzbinu(Posiljka posiljka)
        {

            
            foreach (var i in posiljka.Proizvodi)
            {
                ProizvodInfo info = new ProizvodInfo();
                var proizvod=ProizvodService.Instance.VratiProizvod(i.Id.ToString());
                var kategorija = KategorijaService.Instance.VratiKategoriju(proizvod.Kategorija.Id.ToString());
                info.Naziv = proizvod.Naziv;
                info.Cena = proizvod.Cena;
                info.NazivKategorije = kategorija.Naziv;
                posiljka.InfoProizvoda.Add(info);

            }

          
            var db = SessionManager.GetMongoDB();
            _posiljke = db.GetCollection<Posiljka>("Posiljke");
            _posiljke.InsertOne(posiljka);

        }

        public List<Posiljka> VratiSveNarudzbine()
        {
            var db = SessionManager.GetMongoDB();
            _posiljke = db.GetCollection<Posiljka>("Posiljke");
            var narudzbine = _posiljke.Find<Posiljka>(x => true).ToList();
            return narudzbine;
        }

        public Posiljka VratiNarudzbinu(string Id)
        {
            var db = SessionManager.GetMongoDB();
            _posiljke = db.GetCollection<Posiljka>("Posiljke");
            var posiljka = _posiljke.Find<Posiljka>(x => true && x.Id == ObjectId.Parse(Id)).FirstOrDefault();
            return posiljka;
        }
    }
}
