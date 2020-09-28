using MarqueesAssistant.API.Dtos;
using AutoMapper;
using MarqueesAssistant.API.Controllers;
using System;
using MarqueesAssistant.API.Models;

namespace MarqueesAssistant.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Worker, WorkerDisplayDto>();
            CreateMap<Event, EventDisplayDto>();
            //  CreateMap<Place, EventDisplayDto>().ForMember(x=> x.PlaceName, a => a.MapFrom(s => s.Town));
            CreateMap<Place, PlaceTownDto>();
        }


    }
}