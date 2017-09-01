using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Stash.DataSource.Entities;
using Stash.Services.Logic;
using Stash.Services.RequestModels.Users;
using StashWebApiApp.Models;

namespace StashWebApiApp.Controllers
{
    [RoutePrefix("v1/users")]
    public class UsersController : ApiController
    {
        private readonly UsersLogic _userLogic =  new UsersLogic(new Stash.DataSource.Logic.UserSessionDataSourceLogic());
        //GET v1/users/query
        public async Task<IHttpActionResult> Get(string query)
        {
            return Ok(await _userLogic.GetUsersByQuery(query));
        }

        //GET v1/users
        public async Task<IHttpActionResult> GetAllUsers()
        {
            return Ok(await _userLogic.GetAllUsers());
        }

        //POST v1/users
        public async Task<IHttpActionResult> CreateUser(CreateUserBindingModel request)
        {
            if(request == null)
                ModelState.AddModelError("", "Request parameters not entered.");

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(r => r.Errors).Select(error => error.ErrorMessage).ToList();
                return Content((HttpStatusCode) 422, new {errors});
            }

            try
            {
                var user = await _userLogic.CreateUser(Mapper.Map<Users>(request));
                return Created("", Mapper.Map<UserResponseItem>(user));
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("CreateRandomUsers")]
        public async Task<IHttpActionResult> CreateRandomUsers(int numUsers = 1000)
        {
            var users = await _userLogic.CreateRandomUsers(numUsers);
            return Ok(users);
        }
    }
}
