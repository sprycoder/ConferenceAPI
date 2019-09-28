using AutoMapper;
using Conference.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Conference.API.Automapper.Profiles
{
    public class APIMappingProfile : Profile
    {
        public APIMappingProfile()
        {
            int abc = 0;
            CreateMap<SessionResult, Item>().ReverseMap()
                .ForMember(dest => dest.SessionID, opt => opt.MapFrom<int>(src => Convert.ToInt32(src.href.Substring(src.href.LastIndexOf("/") + 1))))
                .ForMember(dest => dest.Title, opt => opt.MapFrom<string>(src => src.data.FirstOrDefault(x => x.name.Equals("Title")).value))
                .ForMember(dest => dest.Desciption, opt => opt.MapFrom<string>(src => src.data.FirstOrDefault(x => x.name.Equals("Title")).value))
                .ForMember(dest => dest.TimeSlot, opt => opt.MapFrom<string>(src => src.data.FirstOrDefault(x => x.name.Equals("Timeslot")).value))
                .ForMember(dest => dest.Speaker, opt => opt.MapFrom<string>(src => src.data.FirstOrDefault(x => x.name.Equals("Speaker")).value))
                .ForMember(dest => dest.Links, opt => opt.MapFrom<List<LinkResult>>(src => src.links.Select(x => new LinkResult { Rel = x.rel, Href = x.href }).ToList()));

            CreateMap<SpeakerResult, Item>().ReverseMap()
                .ForMember(dest => dest.SpeakerID, opt => opt.MapFrom<int>(src => Convert.ToInt32(src.href.Substring(src.href.LastIndexOf("/") + 1))))
                .ForMember(dest => dest.SpeakerName, opt => opt.MapFrom<string>(src => src.data.FirstOrDefault().value))
                .ForMember(dest => dest.Links, opt => opt.MapFrom<List<LinkResult>>(src => src.links.Select(x => new LinkResult { Rel = x.rel, Href = x.href }).ToList()));
        }
    }
}
