using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeRofit.Business.Models.EntitiesModels;
using TradeRofit.Core.Requests;
using TradeRofit.Entities.Models;

namespace TradeRofit.API.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserSignUpRequest>();
            CreateMap<UserSignUpRequest, User>();
            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();
        }
    }
}
