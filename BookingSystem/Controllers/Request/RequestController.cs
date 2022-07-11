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
            bool isAggregated = false;
            return requestDAO.FindOne(trackingId, isAggregated);
        }

        [Route("api/request-aggregated/{trackingId}")]
        [HttpGet]

        public dynamic GetRequestAggregated(string trackingId)
        {
            bool isAggregated = true;
            return requestDAO.FindOne(trackingId, isAggregated);
        }

        [Route("api/request")]
        [HttpPatch]
        public dynamic UpdateRequest([FromBody] RequestUpdateModel body)
        {
            string token = Convert.ToString(Request.Headers.Authorization);
            bool doesIdExist = (body.Id > 0);

            if (!doesIdExist) ModelState.AddModelError("Id", "Data sent must have an Id");

            if (AuthManager.VerifyToken(token) && AuthManager.VerifyRole(token))
            {
                return requestDAO.UpdateOne(body, ModelState.IsValid);
            }

            Debug.WriteLine("Unauthorized");
            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        [Route("api/request/{trackingId}")]
        [HttpDelete]
        public dynamic DeleteRequest(string trackingId)
        {
            string token = Convert.ToString(Request.Headers.Authorization);

            if (AuthManager.VerifyToken(token) && AuthManager.VerifyRole(token))
            {
                return requestDAO.DeleteOne(trackingId);
            }

            Debug.WriteLine("Unauthorized");
            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        [Route("api/request")]
        [HttpGet]
        public dynamic GetAll()
        {
            string token = Convert.ToString(Request.Headers.Authorization);

            if (AuthManager.VerifyToken(token))
            {
                bool isAggregated = false;
                return requestDAO.GetAll(isAggregated);
            }

            Debug.WriteLine("Unauthorized");
            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        [Route("api/request-aggregated")]
        [HttpGet]
        public dynamic GetAllRequestAggregated()
        {
            string token = Convert.ToString(Request.Headers.Authorization);

            if (AuthManager.VerifyToken(token))
            {
                bool isAggregated = true;
                return requestDAO.GetAll(isAggregated);
            }

            Debug.WriteLine("Unauthorized");
            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        [Route("api/all-request/status/{statusId:int}")]
        [HttpGet]
        public dynamic GetAllRequestByStatus(int statusId)
        {
            string token = Convert.ToString(Request.Headers.Authorization);

            if (AuthManager.VerifyToken(token))
            {
                bool isAggregated = false;
                return requestDAO.GetByStatus(statusId, isAggregated);
            }

            Debug.WriteLine("Unauthorized");
            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        [Route("api/all-request-aggregated/status/{statusId:int}")]
        [HttpGet]
        public dynamic GetAllRequestAggregatedByStatus(int statusId)
        {
            string token = Convert.ToString(Request.Headers.Authorization);

            if (AuthManager.VerifyToken(token))
            {
                bool isAggregated = true;
                return requestDAO.GetByStatus(statusId, isAggregated);
            }

            Debug.WriteLine("Unauthorized");
            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        [Route("api/all-request/office/{officeId:int}")]
        [HttpGet]
        public dynamic GetAllRequestByOffice(int officeId)
        {
            string token = Convert.ToString(Request.Headers.Authorization);

            if (AuthManager.VerifyToken(token))
            {
                Debug.WriteLine(officeId);
                bool isAggregated = false;
                return requestDAO.GetByOffice(officeId, isAggregated);
            }

            Debug.WriteLine("Unauthorized");
            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        [Route("api/all-request-aggregated/office/{officeId:int}")]
        [HttpGet]
        public dynamic GetAllRequestAggregatedByOffice(int officeId)
        {
            string token = Convert.ToString(Request.Headers.Authorization);

            if (AuthManager.VerifyToken(token))
            {
                bool isAggregated = true;
                return requestDAO.GetByOffice(officeId, isAggregated);
            }

            Debug.WriteLine("Unauthorized");
            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        [Route("api/all-request/service/{serviceId:int}")]
        [HttpGet]
        public dynamic GetAllRequestByService(int serviceId)
        {
            string token = Convert.ToString(Request.Headers.Authorization);

            if (AuthManager.VerifyToken(token))
            {
                bool isAggregated = false;
                return requestDAO.GetByService(serviceId, isAggregated);
            }

            Debug.WriteLine("Unauthorized");
            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        [Route("api/all-request-aggregated/service/{serviceId:int}")]
        [HttpGet]
        public dynamic GetAllRequestAggregatedByService(int serviceId)
        {
            string token = Convert.ToString(Request.Headers.Authorization);

            if (AuthManager.VerifyToken(token))
            {
                bool isAggregated = true;
                return requestDAO.GetByService(serviceId, isAggregated);
            }

            Debug.WriteLine("Unauthorized");
            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

    }
}
