using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LagaltAPI.Context;
using LagaltAPI.Models;
using AutoMapper;
using LagaltAPI.Models.DTOs.User;
using LagaltAPI.Repositories;

namespace LagaltAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _service;
        private readonly IMapper _mapper;

        public UsersController(UserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserReadDTO>>> GetUsers()
        {
            return _mapper.Map<List<UserReadDTO>>(await _service.GetAllAsync());
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserReadDTO>> GetUser(int id)
        {
            try
            {
                var user = await _service.GetByIdAsync(id);

                if (user != null)
                    return _mapper.Map<UserReadDTO>(user);
                else
                    return NotFound();
            }
            catch (ArgumentNullException) { return BadRequest(); }
            catch (InvalidOperationException) { return NotFound(); }
        }
        
        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserEditDTO user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            if (!_service.EntityExists(id))
            {
                return NotFound();
            }

            User domainUser = _mapper.Map<User>(user);

            try
            {
                await _service.UpdateAsync(domainUser);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_service.EntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserCreateDTO>> PostUser(UserCreateDTO dtoUser)
        {
            User domainUser = _mapper.Map<User>(dtoUser);
            await _service.AddAsync(domainUser);

            return CreatedAtAction("GetUser", 
                new { id = domainUser.Id }, 
                _mapper.Map<UserReadDTO>(domainUser));
        }

        /* NOT CURRENTLY SUPPORTED */
        /*
        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (!_service.EntityExists(id)) {
                return NotFound();
            }

            await _service.DeleteAsync(id);

            return NoContent();
        }
        */
    }
}
