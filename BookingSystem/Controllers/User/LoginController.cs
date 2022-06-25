using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookingSystem.Controllers.User
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {
        public LoginDAO loginDAO = new LoginDAO();

        [Route("api/login/create")]
        [HttpPost]
        public string Createlogin([FromBody]UserModel body)
        {
            return loginDAO.InsertOne(body);
        }

        [Route("api/login/get/{Id}")]
        [HttpGet]
        public List<UserModel> Getlogin(string Id)
        {
            return loginDAO.FindOne(Id);
        }

        [Route("api/login/update")]
        [HttpPost]
        public string Updatelogin([FromBody] UserModel body)
        {
            return loginDAO.UpdateOne(body);
        }

        [Route("api/login/delete/{Id}")]
        [HttpDelete]
        public string Deletelogin(string Id)
        {
            return loginDAO.DeleteOne(Id);
        }

    }
}
