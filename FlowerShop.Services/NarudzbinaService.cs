using FlowerShop.Entities;
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
            var db = SessionManager.GetMongoDB();
            _posiljke = db.GetCollection<Posiljka>("Posiljke");
            _posiljke.InsertOne(posiljka);

        }
    }
}
