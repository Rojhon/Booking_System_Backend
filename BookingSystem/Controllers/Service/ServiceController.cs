using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using BookingSystem.Models.Services;
using BookingSystem.Data.Service;
using BookingSystem.Helper;
using System.Diagnostics;

namespace BookingSystem.Controllers.Service
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ServiceController : ApiController
    {
        public ServiceDAO serviceDAO = new ServiceDAO();

        [Route("api/service")]
        [HttpPost]
        public dynamic CreateService([FromBody]ServiceModel body)
        {
            string token = Convert.ToString(Request.Headers.Authorization);

            if (AuthManager.VerifyToken(token) && AuthManager.VerifyRole(token))
            {
                return serviceDAO.InsertOne(body, ModelState.IsValid);
            }

            Debug.WriteLine("Unauthorized");
            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        [Route("api/service/{Id}")]
        [HttpGet]
        public dynamic GetService(string Id)
        {
            string token = Convert.ToString(Request.Headers.Authorization);

            if (AuthManager.VerifyToken(token))
            {
                return serviceDAO.FindOne(Id);
            }

            Debug.WriteLine("Unauthorized");
            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        [Route("api/service")]
        [HttpPatch]
        public dynamic UpdateService([FromBody] ServiceModel body)
        {
            string token = Convert.ToString(Request.Headers.Authorization);

            if (AuthManager.VerifyToken(token))
            {
                bool doesIdExist = (body.Id > 0);
                if (!doesIdExist) ModelState.AddModelError("Id", "Data sent must have an Id");
                return serviceDAO.UpdateOne(body, ModelState.IsValid);
            }

            Debug.WriteLine("Unauthorized");
            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        [Route("api/service/{Id}")]
        [HttpDelete]
        public dynamic DeleteService(string Id)
        {
            string token = Convert.ToString(Request.Headers.Authorization);

            if (AuthManager.VerifyToken(token))
            {
                return serviceDAO.DeleteOne(Id);
            }

            Debug.WriteLine("Unauthorized");
            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        [Route("api/student-service")]
        [HttpGet]
        public List<ServiceModel> GetAllStudentService()
        {
            List<ServiceModel> officeModels = serviceDAO.GetAll();
            return officeModels;
        }

        [Route("api/service")]
        [HttpGet]
        public dynamic GetAllServices()
        {
            string token = Convert.ToString(Request.Headers.Authorization);

            if (AuthManager.VerifyToken(token))
            {
                List<ServiceModel> officeModels = serviceDAO.GetAll();
                return officeModels;
            }

            Debug.WriteLine("Unauthorized");
            return Request.CreateResponse(HttpStatusCode.Unauthorized);

        }
    }
}
