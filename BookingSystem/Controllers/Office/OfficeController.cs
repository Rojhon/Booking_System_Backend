using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using BookingSystem.Models.Offices;
using BookingSystem.Data.Office;
using System.Diagnostics;

namespace BookingSystem.Controllers.Offices
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class OfficeController : ApiController
    {
        public OfficeDAO officeDAO = new OfficeDAO();

        [Route("api/office")]
        [HttpPost]
        public string CreateOffice([FromBody]OfficeModel body)
        {
            return officeDAO.InsertOne(body, ModelState.IsValid);
        }

        [Route("api/office/{Id}")]
        [HttpGet]
        public List<OfficeModel> GetOffice(string Id)
        {
            return officeDAO.FindOne(Id);
        }

        [Route("api/office")]
        [HttpPatch]
        public string UpdateOffice([FromBody] OfficeModel body)
        {

            bool doesIdExist = (body.Id > 0);
            if (!doesIdExist) ModelState.AddModelError("Id", "Data sent must have an Id");
            return officeDAO.UpdateOne(body, ModelState.IsValid);
        }

        [Route("api/office/{Id}")]
        [HttpDelete]
        public string DeleteOffice(string Id)
        {
            //System.Diagnostics.Debug.WriteLine(body);
            return officeDAO.DeleteOne(Id);
        }

        [Route("api/office")]
        [HttpGet]
        public List<OfficeModel> GetAllOffices()
        {
            List<OfficeModel> officeModels = officeDAO.GetAll();
            return officeModels;
        }
    }
}
