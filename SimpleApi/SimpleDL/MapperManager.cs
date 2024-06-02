using AutoMapper;
using SimpleDB.EF.Models;
using SimpleEntites;


namespace SimpleDB
{
    public class MapperManager : Profile
    {
        public MapperManager()
        {
            CreateMap<UserLoginDTO, User>()
                .ForMember(user => user.UserName,
                options => options.MapFrom(userLoginDTO => userLoginDTO.Name));
            //.ForMember(x => x.UserName, options => options.Ignore());
        }
    }
}
