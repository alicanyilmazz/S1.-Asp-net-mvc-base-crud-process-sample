using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace alican_yılmaz_come_back.Models.Managers
{
    public class DatabaseContext:DbContext 
    {
        public DbSet<kisiler> kisiler { get; set; }
        public DbSet<adresler> adresler { get; set; }

        public DatabaseContext()
        {
            Database.SetInitializer(new veritabanı_olusturucu());
        }
         
    }

    public class veritabanı_olusturucu:CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {   
            //kisiler insert ediliyor
            for (int i = 0; i < 15; i++)
            {
                kisiler kisi = new kisiler();
                kisi.ad = FakeData.NameData.GetFirstName();
                kisi.soyad = FakeData.NameData.GetSurname();
                kisi.yas = FakeData.NumberData.GetNumber(10, 90);

                context.kisiler.Add(kisi);

            }

            context.SaveChanges();


            //adresler insert ediliyor

            List<kisiler> tum_kisiler = context.kisiler.ToList();
            foreach (kisiler kisi in tum_kisiler)
            {
                for (int i = 0; i < FakeData.NumberData.GetNumber(1,5); i++)
                {
                    adresler adres = new adresler();
                    adres.adres_tanim = FakeData.PlaceData.GetAddress();
                    adres.kisi = kisi;

                    context.adresler.Add(adres);
                }
            }
            context.SaveChanges();
        }
    }
}