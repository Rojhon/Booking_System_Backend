using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using BookingSystem.Models.Test;
using BookingSystem.Data.Test;

namespace BookingSystem.Controllers.Test
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TestController : ApiController
    {
        public TestDAO testDAO = new TestDAO();

        [Route("api/test/create-test")]
        [HttpPost]
        public TestModel CreateTest([FromBody] TestModel body)
        {
            //System.Diagnostics.Debug.WriteLine(body);
            testDAO.Create(body);
            return body;
        }

        [Route("api/test/delete-test/{id}")]
        [HttpPost]
        public string DeleteTest(int id)
        {
            //System.Diagnostics.Debug.WriteLine(body);
            testDAO.Delete(id);
            return "Test deleted!!!";
        }

        [Route("api/test/update-test")]
        [HttpPost]
        public TestModel UpdateTest([FromBody] TestModel body)
        {
            //System.Diagnostics.Debug.WriteLine(body);
            testDAO.Update(body);
            return body;
        }

        [Route("api/test/details-test/{id}")]
        [HttpGet]
        public TestModel DetailsTest(int id)
        {
            //System.Diagnostics.Debug.WriteLine(body);
            TestModel testModel = testDAO.Details(id);
            return testModel;
        }

        [Route("api/test/get-all-test")]
        [HttpGet]
        public List<TestModel> GetAllTest()
        {
            List<TestModel> testModel = testDAO.GetAll();
            return testModel;
        }
    }
}
