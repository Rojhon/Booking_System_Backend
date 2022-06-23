using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookingSystem.Controllers.User
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RegisterController : ApiController
    {
        public RegisterDAO registerDAO = new RegisterDAO();

        [Route("api/users/create")]
        [HttpPost]
        public string CreateUser([FromBody]UserModel body)
        {
            return registerDAO.InsertOne(body);
        }

        [Route("api/users/get/{Id}")]
        [HttpGet]
        public List<UserModel> GetUser(string Id)
        {
            return registerDAO.FindOne(Id);
        }

        [Route("api/users/update")]
        [HttpPost]
        public string UpdateUser([FromBody] UserModel body)
        {
            return registerDAO.UpdateOne(body);
        }

        [Route("api/users/delete/{Id}")]
        [HttpDelete]
        public string DeleteUser(string Id)
        {
            return registerDAO.DeleteOne(Id);
        }

    }
}
