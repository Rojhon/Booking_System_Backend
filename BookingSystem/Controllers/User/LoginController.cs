using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using BookingSystem.Models.Users;
using BookingSystem.Data.User;

namespace BookingSystem.Controllers.User
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {
        public LoginDAO loginDAO = new LoginDAO();

        [Route("api/login/{Id}")]
        [HttpGet]
        public List<UserModel> Getlogin(string Id)
        {
            return loginDAO.FindOne(Id);
        }

        [Route("api/login")]
        [HttpPost]
        public string Updatelogin([FromBody] UserModel body)
        {
            return loginDAO.UpdateOne(body);
        }
    }
}
