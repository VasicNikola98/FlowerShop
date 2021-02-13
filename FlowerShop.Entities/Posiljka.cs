using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Entities
{
    public class Posiljka
    {
        public ObjectId Id { get; set; }
        public DateTime DatumPorucivanja { get; set; }
        public float UkupnaCena { get; set; }
        public List<MongoDBRef> Proizvodi { get; set; }
        public List<ProizvodInfo> InfoProizvoda { get; set; }


        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Adresa { get; set; }
        public string BrojTelefona { get; set; }
        public string Grad { get; set; }
        public string Drzava { get; set; }
        public string PostanskiBroj { get; set; }


        public Posiljka()
        {
            Proizvodi = new List<MongoDBRef>();
            InfoProizvoda = new List<ProizvodInfo>();
        }
        
    }
}
