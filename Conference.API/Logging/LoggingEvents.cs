using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conference.API.Logging
{
    public class LoggingEvents
    {
        public const int ListSessions = 1000;
        public const int GetSession = 1001;

        public const int GetSessionNotFound = 4000;
    }
}
