using AutoMapper;
using LagaltAPI.Models.Domain;
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

        /// <summary> Fetches a message from the database based on message id. </summary>
        /// <param name="messageId"> The id of the message to retrieve. </param>
        /// <returns>
        ///     A read-specific DTO of the message if it is found in the database.
        ///     If it is not, then NotFound is returned instead.
        /// </returns>
        // GET: api/Messages/5
        [HttpGet("{messageId}")]
        public async Task<ActionResult<MessageReadDTO>> GetMessage(int messageId)
        {
            try
            {
                var domainMessage = await _service.GetByIdAsync(messageId);

                if (domainMessage != null)
                    return _mapper.Map<MessageReadDTO>(domainMessage);
                else
                    return NotFound();
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        /// <summary> Fetches messages from the database based on project id. </summary>
        /// <param name="projectId"> The id of the project to retrieve messages from. </param>
        /// <returns> An enumerable containing read-specific DTOs of the messages. </returns>
        // GET: api/Messages/Project/5
        [HttpGet("Project/{projectId}")]
        public async Task<ActionResult<IEnumerable<MessageReadDTO>>> GetProjectMessages
            (int projectId)
        {
            return _mapper.Map<List<MessageReadDTO>>(
                await _service.GetByProjectIdAsync(projectId));
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
            domainMessage = await _service.AddAsync(domainMessage);

            return CreatedAtAction("GetMessage", 
                new { messageId = domainMessage.Id }, 
                _mapper.Map<MessageReadDTO>(domainMessage));
        }
    }
}
