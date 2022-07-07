using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using BookingSystem.Models.Services;
using BookingSystem.Data.Service;

namespace BookingSystem.Controllers.Service
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ServiceController : ApiController
    {
        public ServiceDAO serviceDAO = new ServiceDAO();

        [Route("api/service")]
        [HttpPost]
        public string CreateOffice([FromBody]ServiceModel body)
        {
            return serviceDAO.InsertOne(body, ModelState.IsValid);
        }

        [Route("api/service/{Id}")]
        [HttpGet]
        public dynamic GetOffice(string Id)
        {
            return serviceDAO.FindOne(Id);
        }

        [Route("api/service")]
        [HttpPatch]
        public string UpdateOffice([FromBody] ServiceModel body)
        {
            bool doesIdExist = (body.Id > 0);
            if (!doesIdExist) ModelState.AddModelError("Id", "Data sent must have an Id");
            return serviceDAO.UpdateOne(body, ModelState.IsValid);
        }

        [Route("api/service/{Id}")]
        [HttpDelete]
        public string DeleteOffice(string Id)
        {
            return serviceDAO.DeleteOne(Id);
        }

        [Route("api/service")]
        [HttpGet]
        public List<ServiceModel> GetAllOffices()
        {
            List<ServiceModel> officeModels = serviceDAO.GetAll();
            return officeModels;
        }
    }
}
