using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UserDataAccess1;

namespace TodoList.Controllers
{
    public class UsersController : ApiController
    {
        // GET api/users
        [HttpGet]
        public HttpResponseMessage GetAllUsers()
        {
            using (TodoAppDBEntities dbEntities = new TodoAppDBEntities())
            {
                var users = dbEntities.Users.ToList();
                return Request.CreateResponse(HttpStatusCode.OK, users);
            }
        }

        // GET api/users/5
        [HttpGet]
        public HttpResponseMessage GetUserById(int id)
        {
            using (TodoAppDBEntities dbEntities = new TodoAppDBEntities())
            {
                var user = dbEntities.Users.FirstOrDefault(x => x.Id == id);
                if (user != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, user);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User with ID " + id.ToString() + " not found.");
                }
            }
        }

        // POST api/users
        [HttpPost]
        public HttpResponseMessage CreateUser([FromBody] User user)
        {
            

            try
            {
                using (TodoAppDBEntities dbEntities = new TodoAppDBEntities())
                {
                    dbEntities.Users.Add(user);
                    dbEntities.SaveChanges();

                    
                    var message = Request.CreateResponse(HttpStatusCode.Created, user);
                    
                    message.Headers.Location = new Uri(Request.RequestUri + "/" + user.Id.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
               
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "An error occurred: " + ex.Message);
            }
        }


        // DELETE api/users/5
        [HttpDelete]
        public HttpResponseMessage DeleteUserById(int id)
        {
            try
            {
                using (TodoAppDBEntities dbEntities = new TodoAppDBEntities())
                {
                    var entity = dbEntities.Users.FirstOrDefault(x => x.Id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User with ID " + id.ToString() + " not found.");
                    }
                    else
                    {
                        dbEntities.Users.Remove(entity);
                        dbEntities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        // PUT api/users/5
        [HttpPut]
        public HttpResponseMessage UpdateUserById(int id, [FromBody] User user)
        {
            

            try
            {
                using (TodoAppDBEntities entities = new TodoAppDBEntities())
                {
                    var entity = entities.Users.FirstOrDefault(u => u.Id == id);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User not found");
                    }

                    entity.Username = user.Username;
                    entity.Password = user.Password;
                    entity.Gender = user.Gender;
                    entity.Birthdate = user.Birthdate;
                    entity.Email = user.Email;
                    entity.Firstname = user.Firstname;
                    entity.Lastname = user.Lastname;
                    entity.Middlename = user.Middlename;
                    entity.Prefix = user.Prefix;
                    entity.Suffix = user.Suffix;

                    entities.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        // GET api/users?username=input&password=input params
        [HttpGet]
        public HttpResponseMessage CheckUserLogin([FromUri]string username, [FromUri] string password)
        {
            try
            {
                using (TodoAppDBEntities dbEntities = new TodoAppDBEntities())
                {
                   var existingUser = dbEntities.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
                    if (existingUser !=null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, new { success = true, message = "Login successful", user = existingUser });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, new { success = false, message = "Invalid username or password" });
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}
