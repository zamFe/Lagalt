using AutoMapper;
using LagaltAPI.Models.Domain;
using LagaltAPI.Models.DTOs.Message;
using LagaltAPI.Models.Wrappers;
using LagaltAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LagaltAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Authorize]
    public class MessagesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly MessageService _service;
        private readonly ProjectService _projectService;
        private readonly UriService _uriService;
        private readonly UserService _userService;

        // Constructor.
        public MessagesController(IMapper mapper, MessageService service,
            ProjectService projectService, UriService uriService, UserService userService)
        {
            _mapper = mapper;
            _service = service;
            _projectService = projectService;
            _uriService = uriService;
            _userService = userService;
        }

        // Could be expanded to implement a profanity filter.
        // Should probably also check that the date is valid.
        private ValidationResult ValidateNewMessage(MessageCreateDTO dtoMessage)
        {
            if (!_userService.UserExists(dtoMessage.UserId))
                return new ValidationResult(false, "Unable to find user");

            if (!_projectService.ProjectExists(dtoMessage.ProjectId))
                return new ValidationResult(false, "Unable to find project");

            if (! _projectService.UserIsProjectMember(dtoMessage.ProjectId, dtoMessage.UserId))
                return new ValidationResult(false, "User is not a member of the project");

            return new ValidationResult(true);
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
            }
            catch (InvalidOperationException) {}
            return NotFound();
        }

        /// <summary>
        ///     Fetches messages from the database based on project id,
        ///     according to the specified offset and limit.
        ///     </summary>
        /// <param name="projectId"> The id of the project to retrieve messages from. </param>
        /// <param name="offset"> Specifies the index of the first message to be included. </param>
        /// <param name="limit"> Specifies how many messages to include. </param>
        /// <returns> An enumerable containing read-specific DTOs of the messages. </returns>
        // GET: api/Messages/Project/5?offset=5&limit=5
        [HttpGet("Project/{projectId}")]
        public async Task<ActionResult<Page<MessageReadDTO>>> GetProjectMessages
            (int projectId, [FromQuery] int offset, [FromQuery] int limit)
        {
            var range = new PageRange(offset, limit);
            var messages = _mapper.Map<List<MessageReadDTO>>(
                await _service.GetPageByProjectIdAsync(projectId, range));
            var totalMessages = await _service.GetTotalProjectMessagesAsync(projectId);
            var baseUri = _uriService.GetBaseUrl() + $"api/Messages/Project/{projectId}";
            return new Page<MessageReadDTO>(messages, totalMessages, range, baseUri);
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
            var validation = ValidateNewMessage(dtoMessage);
            if (!validation.Result)
                return BadRequest(validation.RejectionReason);

            var domainMessage = _mapper.Map<Message>(dtoMessage);
            domainMessage = await _service.AddAsync(domainMessage);
            return CreatedAtAction("GetMessage", 
                new { messageId = domainMessage.Id }, 
                _mapper.Map<MessageReadDTO>(domainMessage));
        }
    }
}
