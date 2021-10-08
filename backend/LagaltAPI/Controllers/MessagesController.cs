using AutoMapper;
using LagaltAPI.Models;
using LagaltAPI.Models.DTOs.Message;
using LagaltAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LagaltAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class MessagesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly MessageService _service;

        // Constructor.
        public MessagesController(IMapper mapper, MessageService service)
        {
            _mapper = mapper;
            _service = service;
        }

        /// <summary> Fetches all available messages from the database. </summary>
        /// <returns> An enumerable containing read-specific DTOs of the messages. </returns>
        // GET: api/Messages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageReadDTO>>> GetMessages()
        {
            return _mapper.Map<List<MessageReadDTO>>(await _service.GetAllAsync());
        }

        /// <summary> Fetches messages from the database based on project id. </summary>
        /// <param name="id"> The id of the project to retrieve messages from. </param>
        /// <returns> An enumerable containing read-specific DTOs of the messages. </returns>
        // GET: api/Messages/Project/5
        [HttpGet("Project/{id}")]
        public async Task<ActionResult<IEnumerable<MessageReadDTO>>> GetProjectMessages(int id)
        {
            return _mapper.Map<List<MessageReadDTO>>(await _service.GetByProjectIdAsync(id));
        }

        /// <summary> Fetches a message from the database based on id. </summary>
        /// <param name="id"> The id of the message to retrieve. </param>
        /// <returns>
        ///     A read-specific DTO of the message if it is found in the database.
        ///     If it is not, then NotFound is returned instead.
        /// </returns>
        // GET: api/Messages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MessageReadDTO>> GetMessage(int id)
        {
            try
            {
                var domainMessage = await _service.GetByIdAsync(id);

                if (domainMessage != null)
                    return _mapper.Map<MessageReadDTO>(domainMessage);
                else
                    return NotFound();
            }
            catch (InvalidOperationException) {
                return NotFound();
            }
        }

        /// <summary> Adds a new message entry to the database. </summary>
        /// <param name="dtoMessage"> A create-specific DTO representing the new message. </param>
        /// <returns>
        ///     A read-specific DTO of the message just added to the database on success,
        ///     or BadRequest on failure.
        /// </returns>
        // POST: api/Messages
        [HttpPost]
        public async Task<ActionResult<MessageReadDTO>> PostMessage(MessageCreateDTO dtoMessage)
        {
            var domainMessage = _mapper.Map<Message>(dtoMessage);
            await _service.AddAsync(domainMessage);

            return CreatedAtAction("GetMessage", 
                new { id = domainMessage.Id }, 
                _mapper.Map<MessageReadDTO>(domainMessage));
        }

        /* TODO - Decide whether or not editing messages will be supported.
         * 
         * // PUT: api/Messages/5
         * [HttpPut("{id}")]
         * public async Task<IActionResult> PutMessage(int id, Message message)
         * {
         *    if (id != message.Id)
         *        return BadRequest();
         * 
         *    _context.Entry(message).State = EntityState.Modified;
         * 
         *    try
         *    {
         *        await _context.SaveChangesAsync();
         *    }
         *    catch (DbUpdateConcurrencyException)
         *    {
         *        if (!MessageExists(id))
         *            return NotFound();
         *        else
         *            throw;
         *    }
         * 
         *    return NoContent();
         * }
         */

        /* TODO - Decide whether or not deleting messages will be supported.
         * 
         * // DELETE: api/Messages/5
         * [HttpDelete("{id}")]
         * public async Task<IActionResult> DeleteMessage(int id)
         * {
         *    var message = await _context.Messages.FindAsync(id);
         *    if (message == null)
         *    {
         *        return NotFound();
         *    }
         * 
         *    _context.Messages.Remove(message);
         *    await _context.SaveChangesAsync();
         * 
         *    return NoContent();
         * }
         * 
         * private bool MessageExists(int id)
         * {
         *    return _context.Messages.Any(e => e.Id == id);
         * }
         */
    }
}
