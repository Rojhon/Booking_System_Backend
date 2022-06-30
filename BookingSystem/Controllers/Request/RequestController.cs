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

        [Route("api/request")]
        [HttpPost]
        public string CreateRequest([FromBody]RequestModel body)
        {
            return requestDAO.InsertOne(body, ModelState.IsValid);
        }

        [Route("api/request/{trackingId}")]
        [HttpGet]
        public List<RequestModel> GetRequest(string trackingId)
        {
            return requestDAO.FindOne(trackingId);
        }

        [Route("api/request")]
        [HttpPatch]
        public string UpdateRequest([FromBody] RequestModel body)
        {
            bool doesIdExist = (body.Id > 0);
            if (!doesIdExist) ModelState.AddModelError("Id", "Data sent must have an Id");
            return requestDAO.UpdateOne(body, ModelState.IsValid);
        }

        [Route("api/request/{trackingId}")]
        [HttpDelete]
        public string DeleteRequest(string trackingId)
        {
            return requestDAO.DeleteOne(trackingId);
        }

        [Route("api/request")]
        [HttpGet]
        public List<RequestModel> GetAll()
        {
            List<RequestModel> requestModels = requestDAO.GetAll();
            return requestModels;
        }

    }
}
