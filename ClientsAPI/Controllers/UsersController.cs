using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ClientsAPI.Models;

namespace ClientsAPI.Controllers
{
    public class UserController : ApiController
    {
        DailyCRUDEntities db = new DailyCRUDEntities();

        [HttpGet]
        [Route("api/Users/GetAllUsers")]
        public IHttpActionResult GetAllUsers()
        {
            return Ok(db.User.Select(u => new
            {
                u.Name,
                Role = u.Role.Name
            }));
        }

        [HttpGet]
        [Route("api/Users/{name}")]
        public IHttpActionResult GetUser(string name)
        {
            var foundUser = db.User.FirstOrDefault(u => u.Name == name);
            if (foundUser == null)
                return BadRequest("Пользователя с таким именем не существует");
            else
                return Ok(new
                {
                    foundUser.Name,
                    Role = foundUser.Role.Name
                });
        }
    }
}
