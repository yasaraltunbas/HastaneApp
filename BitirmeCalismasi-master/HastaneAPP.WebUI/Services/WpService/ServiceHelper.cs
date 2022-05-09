using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using HastaneAPP.HastanebilgiLocal;
using HastaneAPP.WebUI.Models.FormModels;
using Microsoft.EntityFrameworkCore;

namespace HastaneAPP.WebUI.Services.WpService
{
    public class ServiceHelper
    {

        public async Task<List<xAPIDetailModel>> ListxAPIDetail()
        {
            using(var context = new HastaneBilgiContext())
            {
                List<xAPIDetailModel> anaModel = new List<xAPIDetailModel>();
                // xAPIListModel anaModel = new xAPIListModel();
                // xAPIListModel model = new xAPIListModel();
                // string sql = "";
                var model    = await  context.WpH5pxapikatchu.AsQueryable().ToListAsync();
                                    
                foreach(var mdl in model)
                {
                    xAPIDetailModel testmodel = new xAPIDetailModel();
                    testmodel.xapi = mdl;
                    testmodel.xapiActor  = await context.WpH5pxapikatchuActor.Where(x => x.Id ==  mdl.IdActor).FirstOrDefaultAsync();
                    testmodel.xapiObject = await context.WpH5pxapikatchuObject.Where(x => x.Id == mdl.IdObject).FirstOrDefaultAsync();
                    testmodel.xapiVerb   = await context.WpH5pxapikatchuVerb.Where(x => x.Id == mdl.IdVerb).FirstOrDefaultAsync();
                    testmodel.xapiResult = await context.WpH5pxapikatchuResult.Where(x => x.Id == mdl.IdResult).FirstOrDefaultAsync();


                    anaModel.Add(testmodel);
                }
                anaModel = ChangeLanguage(anaModel);

                return anaModel.OrderByDescending(x=> x.xapi.Time).ToList();
            }
        }

        public async Task<List<xAPIDetailModel>> ListxAPIDetailForUser(string mail)
        {
            using(var context = new HastaneBilgiContext())
            {
                List<xAPIDetailModel> anaModel = new List<xAPIDetailModel>();
                // xAPIListModel anaModel = new xAPIListModel();
                // xAPIListModel model = new xAPIListModel();
                // string sql = "";
                var model    = await context.WpH5pxapikatchu.AsQueryable().ToListAsync();
                                    
                foreach(var mdl in model)
                {
                    xAPIDetailModel testmodel = new xAPIDetailModel();
                    testmodel.xapi = mdl;
                    testmodel.xapiActor  = await context.WpH5pxapikatchuActor.Where(x => x.Id ==  mdl.IdActor).FirstOrDefaultAsync();
                    testmodel.xapiObject = await context.WpH5pxapikatchuObject.Where(x => x.Id == mdl.IdObject).FirstOrDefaultAsync();
                    testmodel.xapiVerb   = await context.WpH5pxapikatchuVerb.Where(x => x.Id == mdl.IdVerb).FirstOrDefaultAsync();
                    testmodel.xapiResult = await context.WpH5pxapikatchuResult.Where(x => x.Id == mdl.IdResult).FirstOrDefaultAsync();


                    anaModel.Add(testmodel);
                }
                anaModel = ChangeLanguage(anaModel);

                return anaModel.Where(x => x.xapiActor.ActorId.Contains(mail)).OrderByDescending(x=> x.xapi.Time).ToList();
            }
        }
      

        public void DeletexAPI(int id)
        {
            using(var context = new HastaneBilgiContext())
            {
                var model = context.WpH5pxapikatchu.Find(id);
                context.WpH5pxapikatchu.Remove(model);
                context.SaveChanges();
            }
        }

        public List<xAPIDetailModel> ChangeLanguage(List<xAPIDetailModel> mdl)
        {
            var model = mdl;

            foreach(var item in model)
            {
                if(item.xapiVerb.VerbDisplay == "completed") item.xapiVerb.VerbDisplay = "bitirdi";
                else if(item.xapiVerb.VerbDisplay == "attempted") item.xapiVerb.VerbDisplay = "gördü";
                else if(item.xapiVerb.VerbDisplay == "answered") item.xapiVerb.VerbDisplay = "cevapladı";
                else if(item.xapiVerb.VerbDisplay == "interacted") item.xapiVerb.VerbDisplay = "denedi";
                else if(item.xapiVerb.VerbDisplay == "progressed") item.xapiVerb.VerbDisplay = "ilerledi";
            }
            return model;
        }




        public void CreateUser(WpUsers user, string FirstName, string LastName)
        {
            using(var context = new HastaneBilgiContext())
            {
                context.WpUsers.Add(user);
                context.SaveChanges();
                
                // var model =  context.WpUsers.ToList().LastOrDefault();

                // WpUsermeta userMetaNick = new WpUsermeta();
                // userMetaNick.UserId  = model.Id;
                // userMetaNick.MetaKey = "nickname";
                // userMetaNick.MetaValue = user.UserLogin;

                // context.WpUsermeta.Add(userMetaNick);
                // context.SaveChanges();

                // WpUsermeta userMetaFirst = new WpUsermeta();
                // userMetaFirst.UserId  = model.Id;
                // userMetaFirst.MetaKey = "first_name";
                // userMetaFirst.MetaValue = FirstName;

                // context.WpUsermeta.Add(userMetaFirst);
                // context.SaveChanges();
            
                // WpUsermeta userMetaLast = new WpUsermeta();
                // userMetaLast.UserId  = model.Id;
                // userMetaLast.MetaKey = "last_name";
                // userMetaLast.MetaValue = LastName;

                // context.WpUsermeta.Add(userMetaLast);
                // context.SaveChanges();
               
            }

        }

        public WpUsers GetWpUserByEmail(string email)
        {
            using(var context = new HastaneBilgiContext())
            {
                var model = context.WpUsers.FirstOrDefault(x => x.UserEmail == email);
                return model;
            }
        }

        public WpUsers GetWpUserByName(string name, string surname)
        {
            using(var context = new HastaneBilgiContext())
            {
                
                string username = name + "_" + surname;
                if(name == "Emre" && surname ==  "Onur") 
                {
                    username = "emronr";
                }
                var model = context.WpUsers.FirstOrDefault(x => x.UserNicename == username);
                return model;
            }
        }


        
         public  string Generate(int size)
        {
            // Characters except I, l, O, 1, and 0 to decrease confusion when hand typing tokens
            var charSet = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ23456789";
            var chars = charSet.ToCharArray();
            var data = new byte[1];
            var crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            var result = new StringBuilder(size);
            foreach (var b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }

    } 
}

