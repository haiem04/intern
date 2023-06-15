using demo.DTO;
using demo.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net.Http;
using System.Web.Http;
using demo.CRUD;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using demo.Business;

namespace demo.Controllers
{
    public class SanPhamController : ApiController
    {
        [HttpPost]
        public IHttpActionResult ProductFilter(HttpRequestMessage request)
        {

            var jsonContent = request.Content.ReadAsStringAsync().Result;
            JObject requestBodyObject = JsonConvert.DeserializeObject<JObject>(jsonContent);
            //Pagination result = Pagination.Filter<SanPham, SanPhamDTO>(requestBodyObject["Filter"] as JObject);
            dynamic result = ProductBusiness.FilterProduct(requestBodyObject["Filter"] as JObject);
            return Ok(result);

        }

        [HttpPost]

        public IHttpActionResult UpdateProduct(HttpRequestMessage request)
        {
            var requestBody = request.Content.ReadAsStringAsync().Result;

            dynamic requestBodyObject = JsonConvert.DeserializeObject(requestBody);
            int code = requestBodyObject.code;
            JObject data = requestBodyObject.data;
            try
            {
                if (code == 0)
                {
                    var result = ProductBusiness.CreateProduct(data);
                    return Ok(result);

                    //result = CreateEntity.Create<SanPham, SanPhamDTO>(data);
                }
                else
                {
                    var result = ProductBusiness.UpdateProduct(data);
                    return Ok(result);

                    //result = UpdateEntity.Update<SanPham, SanPhamDTO>(data);
                }
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }


        [HttpPost]

        public IHttpActionResult DeleteProduct(HttpRequestMessage request)
        {
            var requestBody = request.Content.ReadAsStringAsync().Result;

            dynamic requestBodyObject = JsonConvert.DeserializeObject(requestBody);
            JArray idsArray = requestBodyObject.ids;
            return Ok(DeleteEntity.Delete<SanPham,SanPhamDTO>(idsArray));
        }

       

    }
}
