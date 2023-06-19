using demo.Business;
using demo.CRUD;
using demo.DTO;
using demo.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace demo.Controllers
{
    public class DotPhatHanhController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Create(HttpRequestMessage req)
        {
            var reqOjectBody = req.Content.ReadAsStringAsync().Result;
            JObject requestObj = JsonConvert.DeserializeObject<JObject>(reqOjectBody);
            try
            {
                var result = new
                {
                    code =  2,
                    data = DotPhatHanhBusiness.Create(requestObj)
                };

            return Ok(result);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            return BadRequest();
            }

        }

        [HttpPost]
        public  IHttpActionResult Update(HttpRequestMessage requestMessage)
        {
            var reqBody = requestMessage.Content.ReadAsStringAsync().Result;
            dynamic reqBodyObject = JsonConvert.DeserializeObject(reqBody);
            int code = reqBodyObject.code;
            JObject data = reqBodyObject.data;
            try
            {
                if(code == 0)
                {

                    var result = DotPhatHanhBusiness.Create(data);
                    return Ok(result);

                }
                else
                {

                var result = DotPhatHanhBusiness.Update(data);
                return Ok(result);
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IHttpActionResult Delete(HttpRequestMessage req)
        {
            var requestBody = req.Content.ReadAsStringAsync().Result;

            dynamic requestBodyObject = JsonConvert.DeserializeObject(requestBody);
            JArray idsArray = requestBodyObject.ids;
            try
            {
                return Ok(DeleteEntity.Delete<DotPhatHanh, DotPhatHanhDTO>(idsArray));

            }
            catch (Exception e)
            {

                return BadRequest(e.ToString());
            }
        }
    }
}
