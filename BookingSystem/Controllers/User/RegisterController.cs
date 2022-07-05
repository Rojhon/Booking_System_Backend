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
    public class UserController : ApiController
    {
        public UserDAO userDAO = new UserDAO();

        [Route("api/user")]
        [HttpPost]
        public string CreateUser([FromBody]UserModel body)
        {
            return userDAO.InsertOne(body);
        }

        [Route("api/user/{Id}")]
        [HttpGet]
        public List<UserModel> GetUser(string Id)
        {
            return userDAO.FindOne(Id);
        }

        [Route("api/user")]
        [HttpPatch]
        public string UpdateUser([FromBody] UserModel body)
        {
            return userDAO.UpdateOne(body);
        }

        [Route("api/user/{Id}")]
        [HttpDelete]
        public string DeleteUser(string Id)
        {
            return userDAO.DeleteOne(Id);
        }

        [Route("api/user")]
        [HttpGet]
        public List<UserModel> GetAll()
        {
            return userDAO.GetAll();
        }

        [Route("api/user/aggregated")]
        [HttpGet]
        public List<UserAggregatedModel> GetAllAggregated()
        {
            return userDAO.GetAllAggregated();
        }

        [Route("api/user/login")]
        [HttpPost]
        public string Login([FromBody]UserModel body)
        {
            return userDAO.SignIn(body);
        }

        [Route("api/user/logout/{userId}")]
        [HttpPost]
        public string Logout(int userId)
        {
            return userDAO.SignOut(userId);
        }

    }
}
