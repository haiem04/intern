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
using System.Text;
using System.Web;
using System.IO;

namespace demo.Controllers
{
    public class SanPhamController : ApiController
    {
        [HttpPost]
        public IHttpActionResult ProductFilter(HttpRequestMessage request)
        {

            var jsonContent = request.Content.ReadAsStringAsync().Result;
            JObject requestBodyObject = JsonConvert.DeserializeObject<JObject>(jsonContent);
            try
            {
                dynamic result = ProductBusiness.FilterProduct(requestBodyObject);
                return Ok(result);
            }
            catch (Exception e)
            {

                return BadRequest(e.ToString());
            }

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
              
                if(code ==0)
                {
                    var result = ProductBusiness.CreateProduct(data);
                    return Ok(result);
                }
                else
                {
                    var result = ProductBusiness.UpdateProduct(data);
                    return Ok(result);
                }
            }
            catch (Exception e)
            {

                return BadRequest(e.ToString());
            }
           
        }


        [HttpPost]

        public IHttpActionResult DeleteProduct(HttpRequestMessage request)
        {
            var requestBody = request.Content.ReadAsStringAsync().Result;

            dynamic requestBodyObject = JsonConvert.DeserializeObject(requestBody);
            JArray idsArray = requestBodyObject.ids;
            try
            {
                return Ok(DeleteEntity.Delete<SanPham, SanPhamDTO>(idsArray));

            }
            catch (Exception e)
            {

                return BadRequest(e.ToString());
            }
        }

       
        [HttpGet]
        public IHttpActionResult ExportExcel()
        {
            StringBuilder strB=ProductBusiness.ExportExcel();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=export.xls");
            HttpContext.Current.Response.Write(strB.ToString());
            HttpContext.Current.Response.End();
            return Ok(0);
        }
    }
}
