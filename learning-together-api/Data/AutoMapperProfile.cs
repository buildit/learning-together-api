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
            this.CreateMap<UserInterest, DisciplineDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.DisciplineId))
                .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Discipline.Name));
            this.CreateMap<Discipline, DisciplineDto>();
            this.CreateMap<DisciplineDto, Discipline>();
        }
    }
}