using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
namespace mvc.Models
{
    public class Vehicles
    {
        IMongoClient mongoClient = new MongoClient("mongodb://localhost:27017");

        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]

        public string _id { get; set; }
        public DateTime dateTime { get; set; }
        public int id { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }


        public List<Vehicles> Nosql_islemleri(int[] a_id, int sayi)
        {
            int aracid=0;
            var database = mongoClient.GetDatabase("taxidata");
            var collection = database.GetCollection<Vehicles>("tablo");

            aracid = a_id[0];
            var sonuc1 = collection.Find<Vehicles>(a => a.id == aracid).SortByDescending(a => a.dateTime).Limit(1).ToList();
            DateTime tson = sonuc1[0].dateTime.AddMinutes(-20);
            var sonuclar1 = collection.Find<Vehicles>(a => a.dateTime <= sonuc1[0].dateTime && a.dateTime >= tson && a.id == aracid).ToList();

            aracid = a_id[1];
            var sonuc2 = collection.Find<Vehicles>(a => a.id == aracid).SortBy(a => a.dateTime).Limit(1).ToList();
            tson = sonuc2[0].dateTime.AddMinutes(-20);
            var sonuclar2 = collection.Find<Vehicles>(a => a.dateTime <= sonuc2[0].dateTime && a.dateTime >= tson && a.id == aracid).ToList();

            var sonuclar = sonuclar1;
            sonuclar.AddRange(sonuclar2);
            if (sayi == 1)
            {
                return sonuclar1;
            }
            else if (sayi == 2)
            {
                return sonuclar2;
            }
            else if (sayi == 3)
            {
                return sonuclar;
            }

            return sonuclar;
        }

        public List<Vehicles> Aralik_alma(int[] a_id,int sayi,DateTime bas, DateTime bit)
        {
            int aracid = 0;
            var database = mongoClient.GetDatabase("taxidata");
            var collection = database.GetCollection<Vehicles>("tablo");

            aracid = a_id[0];
            var sonuclar1 = collection.Find<Vehicles>(a => a.dateTime <= bit && a.dateTime >= bas && a.id == aracid).ToList();

            aracid = a_id[1];
            var sonuclar2 = collection.Find<Vehicles>(a => a.dateTime <= bit && a.dateTime >= bas && a.id == aracid).ToList();

            var sonuclar = sonuclar1;
            sonuclar.AddRange(sonuclar2);
            if (sayi == 1)
            {
                return sonuclar1;
            }
            else if (sayi == 2)
            {
                return sonuclar2;
            }
            else
            {
                return sonuclar;
            }

        }
    }
}
