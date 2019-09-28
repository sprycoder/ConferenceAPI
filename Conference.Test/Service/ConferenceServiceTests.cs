using AutoMapper;
using AutoMapper.Configuration;
using Conference.API.Automapper.Profiles;
using Conference.API.Model;
using Conference.API.Service;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Conference.Test.Service
{
    public class ConferenceServiceTests
    {
        private readonly IConferenceService _service;
        private readonly IMapper _mapper;

        public ConferenceServiceTests()
        {
            // Arrange
            var mappingProfile = new MapperConfigurationExpression();
            mappingProfile.AddProfile(new APIMappingProfile());

            var config = new MapperConfiguration(mappingProfile);
            _mapper = new Mapper(config);
            _service = new ConferenceService(_mapper);
        }

        [Fact]
        public void GetAllSessions()
        {
            // Act
            IEnumerable<SessionResult> response = (_service.GetAllSessions()).Result;

            // Assert
            Assert.NotNull(response);
            Assert.True(response.ToList().Count > 0);
        }

        [Fact]
        public void GetSessionById_where_sessionid_not_found()
        {
            // Arrange
            int sessionId = int.MaxValue;

            // Act
            SessionResult response = _service.GetSessionById(sessionId).Result;

            // Assert
            Assert.Null(response);
        }

        [Fact]
        public void GetSessionById_where_sessionid_found()
        {
            // Arrange
            int sessionId = 101;

            // Act
            SessionResult response = _service.GetSessionById(sessionId).Result;

            // Assert
            Assert.NotNull(response);
            Assert.Equal(sessionId, response.SessionID);
            Assert.NotNull(response.Title);
            Assert.NotNull(response.Desciption);
            Assert.NotNull(response.Href);
            Assert.NotNull(response.TimeSlot);
            Assert.NotNull(response.Speaker);
            Assert.True(response.Links.Count > 0);
        }
    }
}
