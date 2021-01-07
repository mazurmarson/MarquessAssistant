using MarqueesAssistant.API.Dtos;
using AutoMapper;
using MarqueesAssistant.API.Controllers;
using System;
using MarqueesAssistant.API.Models;
using System.Collections.Generic;

namespace MarqueesAssistant.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Worker, WorkerDisplayDto>();
            CreateMap<Event, EventDisplayDto>();
            CreateMap<Place, PlaceTownDto>();
        }


    }
}