using Conference.API.Logging;
using Conference.API.Model;
using Conference.API.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Conference.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConferenceController : ControllerBase
    {
        private readonly IConferenceService confService;
        private readonly ILogger confLogger;

        public ConferenceController(IConferenceService service, ILogger<ConferenceController> logger)
        {
            confService = service;
            confLogger = logger;
        }

        // GET: api/Conference
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SessionResult>), (int)StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            confLogger.LogInformation(LoggingEvents.ListSessions, "Getting session list");
            return Ok(await confService.GetAllSessions());
        }

        // GET: api/Conference/sessionId
        [HttpGet("{sessionId:int}")]
        [ProducesResponseType((int)StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(SessionResult), (int)StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSessionById(int sessionId)
        {
            confLogger.LogInformation(LoggingEvents.GetSession, "Getting session {sessionId}", sessionId);

            var session = await confService.GetSessionById(sessionId);
            if (session == null)
            {
                confLogger.LogWarning(LoggingEvents.GetSessionNotFound, "GetSessionById({Id}) NOT FOUND", sessionId);
                return NotFound(sessionId);
            }
            else
            {
                return Ok(session);
            }
        }
    }
}
