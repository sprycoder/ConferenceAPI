using Conference.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conference.API.Service
{
    public interface IConferenceService
    {
        Task<IEnumerable<SessionResult>> GetAllSessions();

        Task<SessionResult> GetSessionById(int sessionId);
    }
}
