using AutoMapper;
using LagaltAPI.Models;
using LagaltAPI.Models.DTOs.User;
using LagaltAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LagaltAPI
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserService _service;

        // Constructor.
        public UsersController(IMapper mapper, UserService service)
        {
            _mapper = mapper;
            _service = service;
        }

        /// <summary> Fetches all available users from the database. </summary>
        /// <returns> An enumerable containing read-specific DTOs of the users. </returns>
        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserReadDTO>>> GetUsers()
        {
            return _mapper.Map<List<UserReadDTO>>(await _service.GetAllAsync());
        }

        /// <summary> Fetches a user from the database based on id. </summary>
        /// <param name="id"> The id of the user to retrieve. </param>
        /// <returns>
        ///     A read-specific DTO of the user if it is found in the database.
        ///     If it is not, then NotFound is returned instead.
        /// </returns>
        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserReadDTO>> GetUser(int id)
        {
            try
            {
                var domainUser = await _service.GetByIdAsync(id);

                if (domainUser != null)
                    return _mapper.Map<UserReadDTO>(domainUser);
                else
                    return NotFound();
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        /// <summary>
        ///     Updates the specified user in the database to match the provided DTO.
        /// </summary>
        /// <param name="id"> The id of the user to update. </param>
        /// <param name="dtoUser">
        ///     An edit-specific DTO containing the updated version of the user.
        /// </param>
        /// <returns>
        ///     NoContent on successful database update,
        ///     BadRequest if the provided id and the id of the user do not match,
        ///     or NotFound if the provided id does not match any users in the database.
        /// </returns>
        /// <exception cref="DbUpdateConcurrencyException">
        ///     Thrown when the user is found in the database but not able to be updated.
        /// </exception>
        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserEditDTO dtoUser)
        {
            if (id != dtoUser.Id)
                return BadRequest();

            if (!_service.EntityExists(id))
                return NotFound();

            User domainUser = _mapper.Map<User>(dtoUser);

            try
            {
                await _service.UpdateAsync(domainUser);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_service.EntityExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        /// <summary> Adds a new user entry to the database. </summary>
        /// <param name="dtoUser">
        ///     A creation-specific DTO representing the new user.
        /// </param>
        /// <returns>
        ///     A read-specific DTO of the user just added to the database on success,
        ///     or BadRequest on failure.
        /// </returns>
        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<UserCreateDTO>> PostUser(UserCreateDTO dtoUser)
        {
            User domainUser = _mapper.Map<User>(dtoUser);
            await _service.AddAsync(domainUser);

            return CreatedAtAction("GetUser", 
                new { id = domainUser.Id }, 
                _mapper.Map<UserReadDTO>(domainUser));
        }

        /* TODO - Decide whether or not deleting users will be supported.
         *
         * // DELETE: api/Users/5
         * [HttpDelete("{id}")]
         * public async Task<IActionResult> DeleteUser(int id)
         * {
         *     if (!_service.EntityExists(id)) {
         *         return NotFound();
         *     }
         * 
         *    await _service.DeleteAsync(id);
         * 
         *    return NoContent();
         * }
         */
    }
}
