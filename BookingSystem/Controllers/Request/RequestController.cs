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
            //string token = Convert.ToString(Request.Headers.Authorization);

            //if (AuthManager.VerifyToken(token) && AuthManager.VerifyRole(token))
            //{
            //    return requestDAO.InsertOne(body, ModelState.IsValid);
            //}

            //Debug.WriteLine("Forbidden");
            //return "Forbidden";
            return requestDAO.InsertOne(body, ModelState.IsValid);
        }

        [Route("api/request/{trackingId}")]
        [HttpGet]
        public List<RequestModel> GetRequest(string trackingId)
        {
            //string token = Convert.ToString(Request.Headers.Authorization);

            //if (AuthManager.VerifyToken(token))
            //{
            //    return requestDAO.FindOne(trackingId);
            //}

            //Debug.WriteLine("Forbidden");
            //return new List<RequestModel>();
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
        public List<RequestModel> GetAll()
        {
            string token = Convert.ToString(Request.Headers.Authorization);

            if (AuthManager.VerifyToken(token))
            {
                return requestDAO.GetAll();
            }

            Debug.WriteLine("Forbidden");
            return new List<RequestModel>();
            //return Request.CreateResponse(HttpStatusCode.Forbidden);
        }

    }
}
