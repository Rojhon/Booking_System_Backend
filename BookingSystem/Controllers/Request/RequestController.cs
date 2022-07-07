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
using BookingSystem.Helper;
using System.Diagnostics;

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
        public dynamic GetRequest(string trackingId)
        {
            return requestDAO.FindOne(trackingId);
        }

        [Route("api/request")]
        [HttpPatch]
        public string UpdateRequest([FromBody] RequestModel body)
        {
            string token = Convert.ToString(Request.Headers.Authorization);
            bool doesIdExist = (body.Id > 0);

            if (!doesIdExist) ModelState.AddModelError("Id", "Data sent must have an Id");

            if (AuthManager.VerifyToken(token) && AuthManager.VerifyRole(token))
            {
                return requestDAO.UpdateOne(body, ModelState.IsValid);
            }

            Debug.WriteLine("Forbidden");
            return "Forbidden";
        }

        [Route("api/request/{trackingId}")]
        [HttpDelete]
        public string DeleteRequest(string trackingId)
        {
            string token = Convert.ToString(Request.Headers.Authorization);

            if (AuthManager.VerifyToken(token) && AuthManager.VerifyRole(token))
            {
                return requestDAO.DeleteOne(trackingId);
            }

            Debug.WriteLine("Forbidden");
            return "Forbidden";
        }

        [Route("api/request")]
        [HttpGet]
        public dynamic GetAll()
        {
            string token = Convert.ToString(Request.Headers.Authorization);

            if (AuthManager.VerifyToken(token))
            {
                return requestDAO.GetAll();
            }

            Debug.WriteLine("Unauthorized");
            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        [Route("api/request/status/{statusId:int}")]
        [HttpGet]
        public List<RequestAggregatedToAllModel> GetRequestByStatus(int statusId)
        {
            string token = Convert.ToString(Request.Headers.Authorization);

            if (AuthManager.VerifyToken(token))
            {
                return requestDAO.GetByStatus(statusId);
            }

            Debug.WriteLine("Forbidden");
            return new List<RequestAggregatedToAllModel>();
        }

        [Route("api/request/office/{officeId:int}")]
        [HttpGet]
        public List<RequestAggregatedToAllModel> GetRequestByOffice(int officeId)
        {
            string token = Convert.ToString(Request.Headers.Authorization);

            if (AuthManager.VerifyToken(token))
            {
                return requestDAO.GetByOffice(officeId);
            }

            Debug.WriteLine("Forbidden");
            return new List<RequestAggregatedToAllModel>();
        }

        [Route("api/request/service/{serviceId:int}")]
        [HttpGet]
        public List<RequestAggregatedToAllModel> GetRequestByService(int serviceId)
        {
            string token = Convert.ToString(Request.Headers.Authorization);

            if (AuthManager.VerifyToken(token))
            {
                return requestDAO.GetByService(serviceId);
            }

            Debug.WriteLine("Forbidden");
            return new List<RequestAggregatedToAllModel>();
        }

        [Route("api/request/aggregated")]
        [HttpGet]
        public List<RequestAggregatedToAllModel> GetAllRequestAggregated()
        {
            string token = Convert.ToString(Request.Headers.Authorization);

            if (AuthManager.VerifyToken(token))
            {
                return requestDAO.GetAllAggregated();
            }

            Debug.WriteLine("Forbidden");
            return new List<RequestAggregatedToAllModel>();
        }

    }
}
