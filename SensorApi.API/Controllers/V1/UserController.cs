﻿using AutoMapper;
using SensorApi.API.DataContracts.Requests;
using SensorApi.API.DataContracts;
using SensorApi.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using S = SensorApi.Services.Model;

namespace SensorApi.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/users")]//required for default versioning
    [Route("api/v{version:apiVersion}/users")]
    public class UserController : Controller
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;

#pragma warning disable CS1591
        public UserController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
#pragma warning restore CS1591

        #region GET
        /// <summary>
        /// Comments and descriptions can be added to every endpoint using XML comments.
        /// </summary>
        /// <remarks>
        /// XML comments included in controllers will be extracted and injected in Swagger/OpenAPI file.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<User> Get(string id)
        {
            var data = await _service.GetAsync(id);

            if (data != null)
                return _mapper.Map<User>(data);
            else
                return null;
        }
        #endregion

        #region POST
        /// <summary>
        /// Creates a user.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="value"></param>
        /// <returns>A newly created user.</returns>
        /// <response code="200">Returns the newly created item.</response>
        [HttpPost]
        [ProducesResponseType(200)]
        public async Task<User> CreateUser([FromBody]UserCreationRequest value)
        {

            //TODO: include exception management policy according to technical specifications
            if (value == null)
                throw new ArgumentNullException("value");

            if (value.User == null)
                throw new ArgumentNullException("value.User");


            var data = await _service.CreateAsync(Mapper.Map<S.User>(value.User));

            if (data != null)
                return _mapper.Map<User>(data);
            else
                return null;

        }
        #endregion

        #region PUT
        [HttpPut()]
        public async Task<bool> UpdateUser(User parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException("parameter");

            return await _service.UpdateAsync(Mapper.Map<S.User>(parameter));
        }
        #endregion

        #region DELETE
        [HttpDelete("{id}")]
        public async Task<bool> DeleteDevice(string id)
        {
            return await _service.DeleteAsync(id);
        }
        #endregion
    }
}
