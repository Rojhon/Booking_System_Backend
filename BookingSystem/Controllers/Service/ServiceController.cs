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

        [Route("api/service/create")]
        [HttpPost]
        public string CreateOffice([FromBody]ServiceModel body)
        {
            return serviceDAO.InsertOne(body);
        }

        [Route("api/service/get/{Id}")]
        [HttpGet]
        public List<ServiceModel> GetOffice(string Id)
        {
            return serviceDAO.FindOne(Id);
        }

        [Route("api/service/update")]
        [HttpPost]
        public string UpdateOffice([FromBody] ServiceModel body)
        {
            //System.Diagnostics.Debug.WriteLine(body);
            return serviceDAO.UpdateOne(body);
        }

        [Route("api/service/delete/{Id}")]
        [HttpDelete]
        public string DeleteOffice(string Id)
        {
            //System.Diagnostics.Debug.WriteLine(body);
            return serviceDAO.DeleteOne(Id);
        }

        [Route("api/service/get-all")]
        [HttpGet]
        public List<ServiceModel> GetAllOffices()
        {
            List<ServiceModel> officeModels = serviceDAO.GetAll();
            return officeModels;
        }
    }
}
