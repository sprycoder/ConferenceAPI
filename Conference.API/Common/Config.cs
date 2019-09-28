using System;
using Microsoft.Extensions.Configuration;

namespace Conference.API.Common
{
    /// <summary>
    /// The configuration settings provider class
    /// </summary>
    public static class Config
    {
        static readonly IConfiguration config;

        static Config()
        {
            config = new ConfigurationBuilder()
                           .AddJsonFile("appsettings.json", true, true)
                           .Build();
        }

        /// <summary>
        /// Provides the API title
        /// </summary>
        /// <returns></returns>
        internal static string ApiTitle()
        {
            return config["AppSettings:API:Title"];
        }

        /// <summary>
        /// Provides the authorized user name
        /// </summary>
        /// <returns></returns>
        public static string GetAuthUserName()
        {
            return config["BasicAuth:UserName"];
        }

        /// <summary>
        /// Provides the authorized user password
        /// </summary>
        /// <returns></returns>
        public static string GetAuthPassword()
        {
            return config["BasicAuth:Password"];
        }

        /// <summary>
        /// Provides the API key name
        /// </summary>
        /// <returns></returns>
        public static string GetApiKeyName()
        {
            return config["ConferenceAPI:ApiKeyName"];
        }

        /// <summary>
        /// Prvides the API key value
        /// </summary>
        /// <returns></returns>
        public static string GetApiKeyValue()
        {
            return config["ConferenceAPI:ApiKeyValue"];
        }

        /// <summary>
        /// Provides the url to get multiple sessions
        /// </summary>
        /// <returns></returns>
        public static string GetSessionsUrl()
        {
            return config["ConferenceAPI:SessionsUrl"];
        }

        /// <summary>
        /// Provides the url to get multiple speakers
        /// </summary>
        /// <returns></returns>
        public static string GetSpeakersUrl()
        {
            return config["ConferenceAPI:SpeakersUrl"];
        }

        /// <summary>
        /// Provides the url to get speaker by provided identifier
        /// </summary>
        /// <param name="speakerId">The speaker identifier</param>
        /// <returns></returns>
        public static string GetSpeakerUrl(int speakerId)
        {
            return string.Format(config["ConferenceAPI:SpeakerUrl"], speakerId);
        }

        /// <summary>
        /// Provides the url to get session by provided identifier
        /// </summary>
        /// <param name="sessionId">The session identifier</param>
        /// <returns></returns>
        public static string GetSessionUrl(int sessionId)
        {
            return string.Format(config["ConferenceAPI:Sessionurl"], sessionId);
        }

        /// <summary>
        /// Is it the demo version
        /// </summary>
        /// <returns>true or false</returns>
        internal static bool IsDemo()
        {
            return Convert.ToBoolean(config["AppSettings:Demo"]);
        }
    }
}
