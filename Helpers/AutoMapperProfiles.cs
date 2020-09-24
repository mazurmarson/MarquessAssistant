using MarqueesAssistant.API.Dtos;
using AutoMapper;
using MarqueesAssistant.API.Controllers;
using System;

namespace MarqueesAssistant.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Worker, WorkerDisplayDto>();
        }


    }
}