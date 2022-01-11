using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Jam3iyati.Models;
using Jam3iyati.Enums;
using HtmlAgilityPack;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Jam3iyati.Controllers;
using System.Threading.Tasks;

namespace Jam3iyati.Helpers
{
    public static class scrapingAdminsHelper
    {
        public async static Task<int> saveAdministration(HtmlNode admin,int code,int key,AdministrationType adminType)
        {
            IAdmin adw;
            int ID = 0;                 

            using (var db = new ApplicationDbContext())
            {
                switch (adminType)
                {
                    case AdministrationType.apc :
                        adw = new Apc()
                        {
                            apcCode = code,
                            DairaID = key
                        };
                        db.Apcs.Add(adw as Apc);
                        db.SaveChanges();
                        ID = ((Apc)adw).ApcID;
                        break;

                    case AdministrationType.daira:
                        adw = new Daira()
                        {
                            dairaCode = code,
                            WilayaID = key
                        };
                        db.Dairas.Add(adw as Daira);
                        db.SaveChanges();
                        ID = ((Daira)adw).DairaID;
                        break;

                    case AdministrationType.wilaya:
                        adw = new Wilaya()
                        {
                            code = code
                        };
                        db.Wilayas.Add(adw as Wilaya);
                        db.SaveChanges();
                        ID = ((Wilaya)adw).WilayaID;
                        break;
                }

                var dbContext = new ApplicationDbContext();
                var userStore = new UserStore<ApplicationUser>(dbContext);
                var appUserManager = new ApplicationUserManager(userStore);

                string modifiedCode;
                if (adminType == AdministrationType.apc) modifiedCode = "apc" + code.ToString();
                else modifiedCode = code.ToString();
                RegisterViewModel model = new RegisterViewModel()
                {
                    Email =  modifiedCode + "@admin.dz",
                    Password = "Admin.1" 
                };

                var user = new ApplicationUser { UserName = model.Email, Email = model.Email  };
                var result = await appUserManager.CreateAsync(user, model.Password);
                

                Administration administration = new Administration()
                {
                    name = admin.InnerHtml,
                    type = adminType,
                    adminID = ID,
                    userID = user.Id
                };

                db.Administrations.Add(administration);
                db.SaveChanges();

                return ID;
            }
        }

        public async static Task scrape()
        {
            string url;
            var web = new HtmlWeb();
            HtmlDocument doc;
            HtmlNode selectNodeDairas;
            HtmlNodeCollection dairas;
            HtmlNode selectNodeCommunes;
            HtmlNodeCollection communes;

            int dairaCode;
            int apcCode;
            int wilayaID;
            int dairaID;

            for (int wilayaCode = 1; wilayaCode < 49; wilayaCode++)
            {
                url = "http://www.interieur.gov.dz/data/comune.php?w=" + wilayaCode;
                doc = web.Load(url);
                selectNodeDairas = doc.GetElementbyId("d");
                dairas = selectNodeDairas.ChildNodes;

                wilayaID = await saveAdministration(dairas[dairas.Count - 1], wilayaCode, 0, AdministrationType.wilaya);

                for (int j = 3; j < dairas.Count; j++)
                {
                    dairaCode = Convert.ToInt32(dairas[j].GetAttributeValue("value", null));
                    dairaID = await saveAdministration(dairas[j], dairaCode, wilayaID, AdministrationType.daira);

                    url = "http://www.interieur.gov.dz/data/comune2.php?d=" + dairaCode;
                    doc = web.Load(url);
                    selectNodeCommunes = doc.GetElementbyId("c");
                    communes = selectNodeCommunes.ChildNodes;

                    for (int k = 3; k < communes.Count; k++)
                    {
                        apcCode = Convert.ToInt32(communes[k].GetAttributeValue("value", null));
                        await saveAdministration(communes[k], apcCode, dairaID, AdministrationType.apc);
                    }
                }

            }
        }
       
    }
}