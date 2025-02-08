using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
using System.Xml.Linq;
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
        [HttpPost]
        [Route("api/Users/Add")]
        public IHttpActionResult PostUser(User user)
        {
            if (user != null)
            {
                db.User.Add(user);
                db.SaveChanges();
            }
            return Ok();
        }

        [HttpPut]
        [Route("api/Users/Edit/{id}")]
        public IHttpActionResult PutUser(int id)
        {
            var user = db.User.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return BadRequest("Пользователь не найден");
            db.Entry(user).State = EntityState.Modified;
            return Ok();
        }

        [HttpDelete]
        [Route("api/Users/Delete/{name}")]
        public IHttpActionResult DeleteUser(string name)
        {
            var user = db.User.FirstOrDefault(u => u.Name == name);
            if (user == null)
                return BadRequest("Пользователь не найден");
            db.User.Remove(user); db.SaveChanges();
            return Ok();
        }
    }
}
