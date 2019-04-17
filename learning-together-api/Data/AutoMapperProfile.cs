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
            this.CreateMap<WorkshopAttendee, AttendeeWorkshopDto>()
                .ForMember(d => d.WorkshopId, opt => opt.MapFrom(src => src.WorkshopId))
                .ForMember(d => d.ImageUrl, opt => opt.MapFrom(src => src.Workshop.ImageUrl))
                .ForMember(d => d.StartDate, opt => opt.MapFrom(src => src.Workshop.Start))
                .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Workshop.Name));
            this.CreateMap<WorkshopAttendee, WorkshopAttendeeDto>()
                .ForMember(d => d.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(d => d.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(d => d.ImageUrl, opt => opt.MapFrom(src => src.User.ImageUrl))
                .ForMember(d => d.FirstName, opt => opt.MapFrom(src => src.User.FirstName));
            this.CreateMap<Discipline, DisciplineDto>();
            this.CreateMap<DisciplineDto, Discipline>();
            this.CreateMap<WorkshopDto, Workshop>();
            this.CreateMap<Workshop, WorkshopDto>();
            this.CreateMap<Workshop, AttendeeWorkshopDto>()
                .ForMember(d => d.WorkshopId, opt => opt.MapFrom(src => src.Id))
                .ForMember(d => d.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}