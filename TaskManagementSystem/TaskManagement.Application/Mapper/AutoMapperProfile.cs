using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TaskManagement.Application.DTO;
using TaskManagement.Domain.Entites;

namespace TaskManagement.Application.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Project, ProjectDto>().ReverseMap();
            CreateMap<TaskItem, TaskDto>().ReverseMap();
            CreateMap<TaskComment, TaskCommentDto>().ReverseMap();
            CreateMap<Company, CompanyDto>().ReverseMap();

            CreateMap<TaskComment, TaskCommentDto>()
            .ForMember(dest => dest.CreatedByUserId, opt => opt.MapFrom(src => src.CreatedByUserId));

            CreateMap<TaskCommentCreateDto, TaskComment>();

        }
    }
}
