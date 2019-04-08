namespace learning_together_api.Data
{
    using AutoMapper;
    using Mappers;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.CreateMap<User, UserDto>();
            this.CreateMap<UserDto, User>();
        }
    }
}