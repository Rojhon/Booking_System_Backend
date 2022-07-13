using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using BookingSystem.Models.Users;
using BookingSystem.Data.User;
using BookingSystem.Helper;
using System.Diagnostics;

namespace BookingSystem.Controllers.User
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        public UserDAO userDAO = new UserDAO();

        [Route("api/user")]
        [HttpPost]
        public dynamic CreateUser([FromBody]UserModel body)
        {
            string token = Convert.ToString(Request.Headers.Authorization);

                if (ModelState.IsValid && AuthManager.VerifyToken(token) && AuthManager.VerifyRole(token))
                {
                    return userDAO.InsertOne(body);
                }

            Debug.WriteLine("Unauthorized");
            return Request.CreateResponse(HttpStatusCode.Unauthorized);

        }

        [Route("api/user/{Id}")]
        [HttpGet]
        public dynamic GetUser(int Id)
        {
            string token = Convert.ToString(Request.Headers.Authorization);

            if (AuthManager.VerifyToken(token))
            {
                return userDAO.FindOne(Id);
            }

            Debug.WriteLine("Unauthorized");
            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        [Route("api/user-aggregated/{Id}")]
        [HttpGet]
        public dynamic GetUserAggregated(int Id)
        {
            string token = Convert.ToString(Request.Headers.Authorization);

            if (AuthManager.VerifyToken(token))
            {
                return userDAO.FindOneAggregated(Id);
            }

            Debug.WriteLine("Unauthorized");
            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        [Route("api/user")]
        [HttpPatch]
        public dynamic UpdateUser([FromBody] UserModel body)
        {
            string token = Convert.ToString(Request.Headers.Authorization);

            if (AuthManager.VerifyToken(token))
            {
                if (ModelState.IsValid)
                {
                    return userDAO.UpdateOne(body);
                }
            }

            Debug.WriteLine("Unauthorized");
            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        [Route("api/user/{Id}")]
        [HttpDelete]
        public dynamic DeleteUser(int Id)
        {
            string token = Convert.ToString(Request.Headers.Authorization);

            if (AuthManager.VerifyToken(token))
            {
                return userDAO.DeleteOne(Id);
            }

            Debug.WriteLine("Unauthorized");
            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        [Route("api/user")]
        [HttpGet]
        public dynamic GetAll()
        {
            string token = Convert.ToString(Request.Headers.Authorization);
            
            if (AuthManager.VerifyToken(token))
            {
                Debug.WriteLine("Unauthorized");
                return userDAO.GetAll();
            }

            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        [Route("api/user-aggregated")]
        [HttpGet]
        public dynamic GetAllAggregated()
        {
            string token = Convert.ToString(Request.Headers.Authorization);

            if (AuthManager.VerifyToken(token))
            {
                return userDAO.GetAllAggregated();
            }

            Debug.WriteLine("Unauthorized");
            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        [Route("api/user/login")]
        [HttpPost]
        public string Login([FromBody]UserModel body)
        {
            return userDAO.SignIn(body);
        }

        [Route("api/user/logout")]
        [HttpPost]
        public dynamic Logout([FromBody] UserModel body)
        {
            string token = Convert.ToString(Request.Headers.Authorization);

            if (AuthManager.VerifyToken(token))
            {
                return userDAO.SignOut(body);
            }

            Debug.WriteLine("Unauthorized");
            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

    }
}
