namespace Jam3iyati.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Jam3iyati.Models;
    using System.Collections;
    using System.Runtime.Serialization.Json;
    using Jam3iyati.Enums;

    internal sealed class Configuration : DbMigrationsConfiguration<Jam3iyati.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Jam3iyati.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            
            string[]  wilayaNames = new string[48] { "Adrar", "Chlef", "Laghouat", "Oum El Bouaghi", "Batna", "B\u00e9ja\u00efa", "Biskra", "B\u00e9char", "Blida", "Bouira", "Tamanrasset", "T\u00e9bessa", "Tlemcen", "Tiaret", "Tizi Ouzou", "Alger", "Djelfa", "Jijel", "S\u00e9tif", "Sa\u00efda", "Skikda", "Sidi Bel Abb\u00e8s", "Annaba", "Guelma", "Constantine", "M\u00e9d\u00e9a", "Mostaganem", "M'Sila", "Mascara", "Ouargla", "Oran", "El Bayadh", "Illizi", "Bordj Bou Arreridj", "Boumerd\u00e8s", "El Tarf", "Tindouf", "Tissemsilt", "El Oued", "Khenchela", "Souk Ahras", "Tipaza", "Mila", "A\u00efn Defla", "Na\u00e2ma", "A\u00efn T\u00e9mouchent", "Gharda\u00efa", "Relizane" };
            Wilaya wilaya;
            Administration[] admins = new Administration[48];

            for (int i = 0; i < 48; i++)
            {
                wilaya = new Wilaya() { code = i + 1 };
                context.Wilayas.AddOrUpdate(wilaya);
                context.SaveChanges();
                context.Administrations.AddOrUpdate(new Administration() {
                    type = AdministrationType.wilaya,
                    name = wilayaNames[i],
                    adminID = wilaya.WilayaID
                                                                         });
                
            }

            
            
        }     
    }
}
