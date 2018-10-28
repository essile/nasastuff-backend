using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using NasasivustonAPI.Models;
using System.Web.Http.Cors;

namespace NasasivustonAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class UsersController : ApiController
    {
        private NasaUserDB db = new NasaUserDB();

        // GET: api/Users
        public IQueryable<User> GetUser()
        {
            return db.User;
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            User user = db.User.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // GET: api/Users/?username=Nick
        [ResponseType(typeof(IEnumerable<User>))]
        public IHttpActionResult GetTuoteNimella(string username)
        {
            return Ok(db.User.Where(x => x.Nickname.Equals(username)));
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.User_id)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            user.RegistrationDate = DateTime.Now;
            user.PasswordHash = user.Password.GetHashCode();

            if (ModelState.IsValid)
            {
                user.Password = "hidden";
                db.User.Add(user);
                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { id = user.User_id }, user);
            }
            else
                return BadRequest(ModelState);

        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.User.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.User.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.User.Count(e => e.User_id == id) > 0;
        }
    }
}