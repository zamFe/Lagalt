using AutoMapper;
using LagaltAPI.Context;
using LagaltAPI.Models;
using LagaltAPI.Models.DTOs.Message;
using LagaltAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LagaltAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly MessageService _service;
        private readonly IMapper _mapper;

        public MessagesController(MessageService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/Messages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageReadDTO>>> GetMessages()
        {
            return _mapper.Map<List<MessageReadDTO>>(await _service.GetAllAsync());
        }

        // GET: api/Messages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MessageReadDTO>> GetMessage(int id)
        {
            try
            {
                var message = await _service.GetByIdAsync(id);

                if (message != null)
                    return _mapper.Map<MessageReadDTO>(message);
                else
                    return NotFound();
            }
            catch (ArgumentNullException) { return BadRequest(); }
            catch (InvalidOperationException) { return NotFound(); }
        }

        /* TODO - decide whether to support editing messages
         * 
        // PUT: api/Messages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessage(int id, Message message)
        {
            if (id != message.Id)
                return BadRequest();

            _context.Entry(message).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }
        */

        // POST: api/Messages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Message>> PostMessage(Message dtoMessage)
        {
            Message domainMessage = _mapper.Map<Message>(dtoMessage);
            await _service.AddAsync(domainMessage);

            return CreatedAtAction("GetMessage", 
                new { id = domainMessage.Id }, 
                _mapper.Map<MessageReadDTO>(domainMessage));
        }

        /* NOT CURRENTLY SUPPORTED */
        /* 
        // DELETE: api/Messages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MessageExists(int id)
        {
            return _context.Messages.Any(e => e.Id == id);
        }
        */
    }
}
