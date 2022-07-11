using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using BookingSystem.Models.Offices;
using BookingSystem.Data.Office;
using BookingSystem.Helper;
using System.Diagnostics;

namespace BookingSystem.Controllers.Offices
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class OfficeController : ApiController
    {
        public OfficeDAO officeDAO = new OfficeDAO();

        [Route("api/office")]
        [HttpPost]
        public dynamic CreateOffice([FromBody]OfficeModel body)
        {
            string token = Convert.ToString(Request.Headers.Authorization);

            if (AuthManager.VerifyToken(token) && AuthManager.VerifyRole(token))
            {
                return officeDAO.InsertOne(body, ModelState.IsValid);
            }

            Debug.WriteLine("Unauthorized");
            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        [Route("api/office/{Id}")]
        [HttpGet]
        public dynamic GetOffice(string Id)
        {
            string token = Convert.ToString(Request.Headers.Authorization);

            if (AuthManager.VerifyToken(token))
            {
                return officeDAO.FindOne(Id);
            }

            Debug.WriteLine("Unauthorized");
            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        [Route("api/office")]
        [HttpPatch]
        public dynamic UpdateOffice([FromBody] OfficeModel body)
        {
            string token = Convert.ToString(Request.Headers.Authorization);

            if (AuthManager.VerifyToken(token))
            {
                bool doesIdExist = (body.Id > 0);
                if (!doesIdExist) ModelState.AddModelError("Id", "Data sent must have an Id");
                return officeDAO.UpdateOne(body, ModelState.IsValid);
            }

            Debug.WriteLine("Unauthorized");
            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        [Route("api/office/{Id}")]
        [HttpDelete]
        public dynamic DeleteOffice(string Id)
        {
            string token = Convert.ToString(Request.Headers.Authorization);

            if (AuthManager.VerifyToken(token))
            {
                return officeDAO.DeleteOne(Id);
            }

            Debug.WriteLine("Unauthorized");
            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        [Route("api/student-office")]
        [HttpGet]
        public List<OfficeModel> GetAllStudentOffices()
        {
            List<OfficeModel> officeModels = officeDAO.GetAll();
            return officeModels;
        }

        [Route("api/office")]
        [HttpGet]
        public dynamic GetAllOffices()
        {

            string token = Convert.ToString(Request.Headers.Authorization);

            if (AuthManager.VerifyToken(token))
            {
                List<OfficeModel> officeModels = officeDAO.GetAll();
                return officeModels;
            }

            Debug.WriteLine("Unauthorized");
            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }
    }
}
