using AutoMapper;
using Employee.Model;
using Employee.Requests;
using Employee.ViewModal;

namespace Employee.Mapper
{
    public class MapperContext : Profile
    {
        public MapperContext() { 
            CreateMap<EmployeesView,Employees>().ReverseMap()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
            CreateMap<Department, DepartmentRequest>().ReverseMap();
        }
    }
}
