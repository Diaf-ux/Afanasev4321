using AutoMapper;
using Afanasev4321.DTOs;
using Afanasev4321.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Teacher, TeacherDTO>().ReverseMap();
        CreateMap<Department, DepartmentDTO>().ReverseMap();
    }
}
