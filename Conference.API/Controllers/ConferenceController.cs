using Conference.API.Model;
using Conference.API.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Conference.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConferenceController : ControllerBase
    {
        private readonly IConferenceService confService;

        public ConferenceController(IConferenceService service)
        {
            confService = service;
        }

        // GET: api/Conference
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SessionResult>), (int)StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            return Ok(await confService.GetAllSessions());
        }

        // GET: api/Conference/sessionId
        [HttpGet("{sessionId:int}")]
        [ProducesResponseType((int)StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(SessionResult), (int)StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSessionById(int sessionId)
        {
            var session = await confService.GetSessionById(sessionId);
            if (session == null)
            {
                return NotFound(sessionId);
            }
            else
            {
                return Ok(session);
            }
        }
    }
}
