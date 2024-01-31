using System;
using AutoMapper;
using Images.Application.Features.Images.Queries;
using Images.Domain.Models;

namespace Images.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ImageDto, Image>().ReverseMap();
    }
}

