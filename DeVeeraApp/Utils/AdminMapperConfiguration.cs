using AutoMapper;
using CRM.Core.Domain;

using CRM.Core.Domain.Users;
using CRM.Core.Domain.VideoModules;

using DeVeeraApp.ViewModels;
using DeVeeraApp.ViewModels.QuestionAnswer;
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

                  .ForMember(dest => dest.AvailableUsers, mo => mo.Ignore());
                 
                   

           

            CreateMap<Level, LevelModel>()
             .ForMember(dest => dest.srno, mo => mo.Ignore());

            CreateMap<Video, VideoModel>()
            .ForMember(dest => dest.FileName, mo => mo.Ignore());


        
            CreateMap<UserRoleModel, UserRole>()
        .ForMember(dest => dest.PermissionRecord_Role_Mapping, mo => mo.Ignore())
        ;


            CreateMap<DashboardQuote, DashboardQuoteModel>()
               .ForMember(dest => dest.VideoModelList, mo => mo.Ignore())
               .ForMember(dest => dest.Menus, mo => mo.Ignore());

            CreateMap<Modules, ModulesModel>()
             .ForMember(dest => dest.QuestionsList, mo => mo.Ignore());

            CreateMap<Questions, QuestionModel>()
           .ForMember(dest => dest.QuestionsList, mo => mo.Ignore())
            .ForMember(dest => dest.AvailableLevels, mo => mo.Ignore())
            .ForMember(dest => dest.AvailableModules, mo => mo.Ignore());


        }
    }
}
