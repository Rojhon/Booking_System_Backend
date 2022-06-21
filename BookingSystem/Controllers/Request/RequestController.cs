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

        [Route("api/request/create-request")]
        [HttpPost]
        public string CreateRequest([FromBody]RequestModel body)
        {
            return requestDAO.InsertOne(body);
        }

        [Route("api/request/get-request/{trackingId}")]
        [HttpGet]
        public List<RequestModel> GetRequest(string trackingId)
        {
            return requestDAO.FindOne(trackingId);
        }

        [Route("api/request/update-request")]
        [HttpPost]
        public string UpdateRequest([FromBody] RequestModel body)
        {
            //System.Diagnostics.Debug.WriteLine(body);
            return requestDAO.UpdateOne(body);
        }

        [Route("api/request/delete-request/{trackingId}")]
        [HttpPost]
        public string DeleteRequest(string trackingId)
        {
            //System.Diagnostics.Debug.WriteLine(body);
            return requestDAO.DeleteOne(trackingId);
        }

        [Route("api/request/get-all-offices")]
        [HttpGet]
        public List<OfficeModel> GetAllOffices()
        {
            List<OfficeModel> officeModels = requestDAO.GetOffices();
            return officeModels;
        }

        [Route("api/request/get-all-services")]
        [HttpGet]
        public List<ServiceModel> GetAllServices()
        {
            List<ServiceModel> serviceModels = requestDAO.GetServices();
            return serviceModels;
        }

        [Route("api/request/get-all-requests")]
        [HttpGet]
        public List<RequestModel> GetAllTest()
        {
            List<RequestModel> requestModels = requestDAO.GetAll();
            return requestModels;
        }

    }
}
