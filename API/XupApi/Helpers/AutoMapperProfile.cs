using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XupApi.Entities;
using XupApi.Models;

namespace XupApi.Helpers
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CheckRegister,CheckRegisterModel>();
            CreateMap<AddCheckModel, CheckRegister>();
            CreateMap<UpdateCheckModel, CheckRegister>();
        }
            
    }
}
