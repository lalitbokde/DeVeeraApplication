﻿using AutoMapper;
using CRM.Core.Domain.Common;
using CRM.Core.Domain.Users;
using DeVeeraApp.Model.Common;
using DeVeeraApp.ViewModels.User;


namespace DeVeeraApp.Utils
{
    public class AdminMapperConfiguration : Profile
    {
        public AdminMapperConfiguration()
        {
          
            //User
            CreateMap<User, UserModel>()
                .ForMember(dest => dest.UserPassword, mo => mo.Ignore())
                .ForMember(dest => dest.AvailableCountries, mo => mo.Ignore())
                 .ForMember(dest => dest.AvailableStates, mo => mo.Ignore())
                  .ForMember(dest => dest.AvailableUsers, mo => mo.Ignore())
                   .ForMember(dest => dest.AvailableUserRoles, mo => mo.Ignore());

            CreateMap<UserModel, User>()             
               .ForMember(dest => dest.Addresses, mo => mo.Ignore());

      


            CreateMap<AddressModel, Address>()
        .ForMember(dest => dest.CustomAttributes, mo => mo.Ignore())
         .ForMember(dest => dest.CreatedOnUtc, mo => mo.Ignore())
          .ForMember(dest => dest.Country, mo => mo.Ignore())
           .ForMember(dest => dest.AddressTypeId, mo => mo.Ignore())
        ;

            CreateMap<Address, AddressModel>()
     .ForMember(dest => dest.AddressHtml, mo => mo.Ignore())
      .ForMember(dest => dest.FormattedCustomAddressAttributes, mo => mo.Ignore())
       .ForMember(dest => dest.AvailableUsers, mo => mo.Ignore())
        .ForMember(dest => dest.AvailableCountries, mo => mo.Ignore())
          .ForMember(dest => dest.AvailableStates, mo => mo.Ignore())
     ;

            CreateMap<UserRoleModel, UserRole>()
        .ForMember(dest => dest.PermissionRecord_Role_Mapping, mo => mo.Ignore())
        ;


          




        }
    }
}