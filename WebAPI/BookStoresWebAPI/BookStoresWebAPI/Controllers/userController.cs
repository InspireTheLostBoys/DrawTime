using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Models.Request;
using WebAPI.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class userController : ControllerBase
    {
        private readonly DBContext _context;

        public userController(DBContext context)
        {
            _context = context;
        }

        // USER LOGIN
        [HttpPost("Login")]
        public LoginResponse Login(LoginRequest loginRequest)
        {
            LoginResponse retval = new LoginResponse();

            try
            {
                var loginCheck = _context.user.Where(x => x.uid.ToUpper() == loginRequest.uid.ToUpper() && x.pin == loginRequest.pin).FirstOrDefault();

                if(loginCheck == null)
                {
                    retval.SetError("Failed to login with the details provided.");
                }
            }
            catch(Exception exc)
            {
                retval.SetError("Failed to login.", exc);
            }

            return retval;
        }

        // GET USERS
        [HttpGet]
        public UserResponse GetAllUsers()
        {
            UserResponse retval = new UserResponse();

            try
            {
                retval.users = _context.user.ToList();
            }
            catch(Exception exc)
            {
                retval.SetError("Failed to get users.", exc);
            }

            return retval;
        }

        // GET USER BY ID
        [HttpGet("{id}")]
        public UserResponse GetUserByID(int id)
        {
            UserResponse retval = new UserResponse();

            try
            {
                user user = _context.user.Where(x => x.id == id).FirstOrDefault();

                if (user == null)
                {
                    retval.SetError($"Failed to find user with id {id}.");
                }
                else
                {
                    retval.user = user;

                    user_role user_Role = _context.user_role.Where(x => x.user_id == user.id).FirstOrDefault();

                    if (user_Role == null)
                    {
                        retval.SetError($"Failed to find user role with id {id}.");
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception exc)
            {
                retval.SetError("Failed to get users.", exc);
            }

            return retval;
        }

        // CREATE USER
        [HttpPost]
        public UserResponse CreateUser(UserRequest userRequest)
        {
            UserResponse retval = new UserResponse();

            try
            {
                // CREATE NEW USER
                _context.user.Add(userRequest.user);
                _context.SaveChanges();
                // ASSIGN USER ROLE
                _context.user_role.Add(new user_role()
                {
                    user_id = userRequest.user.id,
                    role_id_1 = userRequest.role_id
                });
                _context.SaveChanges();
            }
            catch(Exception exc)
            {
                retval.SetError("Failed to create user.", exc);
            }

            return retval;
        }

        // UPDATE USER
        [HttpPut]
        public UserResponse UpdateUser(UserRequest userRequest)
        {
            UserResponse retval = new UserResponse();

            try
            {
                _context.Entry(userRequest.user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception exc)
            {
                retval.SetError("Failed to update user.", exc);
            }

            return retval;
        }

        [HttpDelete]
        public UserResponse DeleteUser(int systemUserID)
        {
            UserResponse retval = new UserResponse();

            try
            {
                user user = _context.user.Find(systemUserID);

                if(user != null)
                {
                    _context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                    _context.SaveChanges();
                }
                else
                {
                    retval.SetError($"Failed to delete user. Could not find user with id {systemUserID}.");
                }
            }
            catch (Exception exc)
            {
                retval.SetError("Failed to delete user.", exc);
            }

            return retval;
        }
    }
}
