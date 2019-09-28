# ConferenceAPI
API built on .net core 2.2 for fetching Session and Speaker data from ConferenceAPI and merge its payload. Also, it provides an endpoint to get session (by filtering the payload) by sessionId. 

Key (technically) features of this API
- Authentication HTTP Basic (credentials are available in appsettings.json file) 
- Unit testing for various scenarios
- Exception handling
- Dependency Injection using Microsoft DI extension
- AutoMapper to map data from one object to another
- Swagger for documentation
- Config class to provide all configred key-values
