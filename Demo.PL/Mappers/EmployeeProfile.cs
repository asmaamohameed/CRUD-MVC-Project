using AutoMapper;
using Demo.DAL.Entites;
using Demo.PL.Models;

namespace Demo.PL.Mappers
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
        }
    }
}
