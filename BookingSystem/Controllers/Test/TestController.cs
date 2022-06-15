using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using BookingSystem.Models.Test;
using BookingSystem.DAO.Test;

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

        [Route("api/test/get-all-test")]
        [HttpGet]
        public List<TestModel> GetAllTest()
        {
            List<TestModel> testModel = testDAO.GetAll();
            return testModel;
        }

        [Route("api/test/get-test")]
        [HttpGet]
        public List<TestModel> GetTest()
        {
            List<TestModel> testModel = new List<TestModel>()
            {
                new TestModel(){ Id = 1, Name="Rojhon" },
                new TestModel(){ Id = 2, Name="Giann" },
                new TestModel(){ Id = 3, Name="Dek" },
                new TestModel(){ Id = 4, Name="Von" }
            };

            return testModel;
        }
    }
}
