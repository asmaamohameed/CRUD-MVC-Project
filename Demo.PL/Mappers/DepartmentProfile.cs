using AutoMapper;
using Demo.DAL.Entites;
using Demo.PL.Models;

namespace Demo.PL.Mappers
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentViewModel>().ReverseMap();
        }
    }
}
