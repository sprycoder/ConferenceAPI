using AutoMapper;
using Conference.API.Common;
using Conference.API.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Conference.API.Service
{
    public class ConferenceService : IConferenceService
    {
        private readonly IMapper _mapper;
        private static List<SessionResult> sessions;

        public ConferenceService(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Provides collection of all the sessions
        /// </summary>
        /// <returns>Collection of sessions</returns>
        public async Task<IEnumerable<SessionResult>> GetAllSessions()
        {
            if (sessions == null || sessions.Count() == 0)
            {
                List<LinkResult> speakerLinks;

                var tskSession = GetSessions();
                var tskSpeaker = GetSpeakers();

                await Task.WhenAll(tskSession, tskSpeaker);

                sessions = (await tskSession).ToList();
                var speakers = (await tskSpeaker).ToList();

                for (int counter = 0; counter < sessions.Count(); counter++)
                {
                    if (!string.IsNullOrEmpty(sessions[counter].Speaker))
                    {
                        speakerLinks = speakers.FirstOrDefault(x => x.SpeakerName.Equals(sessions[counter].Speaker)).Links;
                        if (speakerLinks != null && speakerLinks.Count > 0)
                        {
                            sessions[counter].Links.AddRange(speakerLinks);
                        }
                    }
                }
            }

            return sessions;
        }

        /// <summary>
        /// Provides a session based on it identifier
        /// </summary>
        /// <param name="sessionId">The session identifier</param>
        /// <returns></returns>
        public async Task<SessionResult> GetSessionById(int sessionId)
        {
            SessionResult session = null;

            // filter by sessionId
            session = (await GetAllSessions()).FirstOrDefault(x => x.SessionID.Equals(sessionId));

            if (session != null)
            {
                session.Desciption = await GetSessionByUrl(Config.GetSessionUrl(sessionId));
            }

            return session;
        }

        private async Task<IEnumerable<SessionResult>> GetSessions()
        {
            RootObject response;
            string uri = Config.GetSessionsUrl();

            using (HttpClient client = new HttpClient())
            {
                // Request headers
                client.DefaultRequestHeaders.Add(Config.GetApiKeyName(), Config.GetApiKeyValue());

                HttpResponseMessage apiResponse = await client.GetAsync(uri);

                using (StreamReader reader = new StreamReader(await apiResponse.Content.ReadAsStreamAsync()))
                {
                    response = JsonConvert.DeserializeObject<RootObject>(reader.ReadToEnd());
                }
            }

            return response.collection.items.Select(x => _mapper.Map<SessionResult>(x));
        }

        private async Task<IEnumerable<SpeakerResult>> GetSpeakers()
        {
            RootObject response;
            string uri = Config.GetSpeakersUrl();

            using (HttpClient client = new HttpClient())
            {
                // Request headers
                client.DefaultRequestHeaders.Add(Config.GetApiKeyName(), Config.GetApiKeyValue());

                HttpResponseMessage apiResponse = await client.GetAsync(uri);

                using (StreamReader reader = new StreamReader(await apiResponse.Content.ReadAsStreamAsync()))
                {
                    response = JsonConvert.DeserializeObject<RootObject>(reader.ReadToEnd());
                }
            }

            return response.collection.items.Select(x => _mapper.Map<SpeakerResult>(x));
        }

        private async Task<string> GetSpeakerbyUrl(string speakerUrl)
        {
            if (string.IsNullOrEmpty(speakerUrl))
                return null;
            else
            {
                string speakerUri = Config.IsDemo() ? speakerUrl.Replace("conference", "demo") : speakerUrl;

                string speaker;

                using (HttpClient client = new HttpClient())
                {
                    // Request headers
                    client.DefaultRequestHeaders.Add(Config.GetApiKeyName(), Config.GetApiKeyValue());

                    var apiResponse = await client.GetAsync(speakerUri);

                    using (StreamReader reader = new StreamReader(await apiResponse.Content.ReadAsStreamAsync()))
                    {
                        speaker = reader.ReadToEnd();
                    }
                }
                return speaker;
            }
        }

        private async Task<string> GetSessionByUrl(string sessionUrl)
        {
            if (string.IsNullOrEmpty(sessionUrl))
                return null;
            else
            {
                string sessionUri = Config.IsDemo() ? sessionUrl.Replace("conference", "demo") : sessionUrl;

                string session;

                using (HttpClient client = new HttpClient())
                {
                    // Request headers
                    client.DefaultRequestHeaders.Add(Config.GetApiKeyName(), Config.GetApiKeyValue());

                    var apiResponse = await client.GetAsync(sessionUri);

                    using (StreamReader reader = new StreamReader(await apiResponse.Content.ReadAsStreamAsync()))
                    {
                        session = reader.ReadToEnd();
                    }
                }
                return session;
            }
        }
    }
}
