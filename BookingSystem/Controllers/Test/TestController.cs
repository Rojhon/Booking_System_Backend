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
        public TestModel testModel = new TestModel();
        public TestDAO testDAO = new TestDAO();
    }
}
