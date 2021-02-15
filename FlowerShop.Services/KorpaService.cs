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
    public class KorpaService
    {
        #region Singleton
        public static KorpaService Instance
        {
            get
            {
                if (instance == null) instance = new KorpaService();

                return instance;
            }
        }

        private static KorpaService instance { get; set; }

        private KorpaService()
        {

        }
        #endregion

        private IMongoCollection<Korpa> _korpe;



        public void DodajUKorpu(Korpa korpa,string IdProizvoda)
        {
            var db = SessionManager.GetMongoDB();
            _korpe = db.GetCollection<Korpa>("Korpe");
            var proizvod = ProizvodService.Instance.VratiProizvod(IdProizvoda);
            korpa.Proizvod = new MongoDBRef("Proizvod", proizvod.Id);
            _korpe.InsertOne(korpa);
        }

        public List<Korpa> VratiProizvodeByHashedID(string hashId)
        {
            var db = SessionManager.GetMongoDB();
            _korpe = db.GetCollection<Korpa>("Korpe");
            var mojaKorpa = _korpe.Find<Korpa>(x => true && x.HashUserId.Contains(hashId)).ToList();
            return mojaKorpa;
        }

        public void IzbrisiKorpu(string hashID)
        {
            var db = SessionManager.GetMongoDB();
            _korpe = db.GetCollection<Korpa>("Korpe");
            _korpe.DeleteMany(x => x.HashUserId.Contains(hashID));
        }

        public void IzbrisiIzKorpe(string Id)
        {
            var db = SessionManager.GetMongoDB();
            _korpe = db.GetCollection<Korpa>("Korpe");
            _korpe.DeleteOne(x => x.Id == ObjectId.Parse(Id));
        }

        public void IzbrisiProizvodIzKorpe(string Id)
        {
            var db = SessionManager.GetMongoDB();
            _korpe = db.GetCollection<Korpa>("Korpe");
            _korpe.DeleteMany(x => x.Proizvod.Id == ObjectId.Parse(Id));
        }
    }
}
