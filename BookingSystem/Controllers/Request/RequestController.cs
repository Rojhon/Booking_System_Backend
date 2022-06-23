using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using BookingSystem.Models.Requests;
using BookingSystem.Models.Offices;
using BookingSystem.Models.Services;
using BookingSystem.Data.Request;

namespace BookingSystem.Controllers.Request
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RequestController : ApiController
    {
        public RequestDAO requestDAO = new RequestDAO();

        [Route("api/request/create")]
        [HttpPost]
        public string CreateRequest([FromBody]RequestModel body)
        {
            return requestDAO.InsertOne(body);
        }

        [Route("api/request/get/{trackingId}")]
        [HttpGet]
        public List<RequestModel> GetRequest(string trackingId)
        {
            return requestDAO.FindOne(trackingId);
        }

        [Route("api/request/update")]
        [HttpPost]
        public string UpdateRequest([FromBody] RequestModel body)
        {
            //System.Diagnostics.Debug.WriteLine(body);
            return requestDAO.UpdateOne(body);
        }

        [Route("api/request/delete/{trackingId}")]
        [HttpDelete]
        public string DeleteRequest(string trackingId)
        {
            //System.Diagnostics.Debug.WriteLine(body);
            return requestDAO.DeleteOne(trackingId);
        }

        [Route("api/request/get-all")]
        [HttpGet]
        public List<RequestModel> GetAll()
        {
            List<RequestModel> requestModels = requestDAO.GetAll();
            return requestModels;
        }

    }
}
