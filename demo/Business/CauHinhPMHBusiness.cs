using demo.DTO;
using demo.DTO.PMH.Extensions;
using demo.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace demo.Business
{
    public class CauHinhPMHBusiness
    {
        private static internShipEntities db = new internShipEntities();

        public static CauHinhPMHDTO Create(JObject reqObjectBody)
        {
            CauHinhPMHDTO data = reqObjectBody.ToObject<CauHinhPMHDTO>();
            db.CauHinhPMHs.Add(data.ToModel());
            db.SaveChanges();
            return data;
        }



    }
}