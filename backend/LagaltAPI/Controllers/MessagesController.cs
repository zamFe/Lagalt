using AutoMapper;
using LagaltAPI.Models;
using LagaltAPI.Models.DTOs.Message;
using LagaltAPI.Services;
using Microsoft.AspNetCore.Mvc;
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

        /// <summary> Fetches messages from the database based on project id. </summary>
        /// <param name="id"> The id of the project to retrieve messages from. </param>
        /// <returns> An enumerable containing read-specific DTOs of the messages. </returns>
        // GET: api/Messages/Project/5
        [HttpGet("Project/{id}")]
        public async Task<ActionResult<IEnumerable<MessageReadDTO>>> GetProjectMessages(int id)
        {
            return _mapper.Map<List<MessageReadDTO>>(await _service.GetByProjectIdAsync(id));
        }

        /// <summary> Adds a new message entry to the database. </summary>
        /// <param name="dtoMessage">
        ///     A creation-specific DTO representing the new message.
        /// </param>
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
    }
}
