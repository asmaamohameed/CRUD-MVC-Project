using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Demo.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly MVCAppDBContext context;

        public EmployeeRepository(MVCAppDBContext context): base(context)
        {
            this.context = context;
        }

        public Task<string> GetDepartmentByEmployeeId(int? id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByDepartName(string DeptName) => await context.Employees.Where(e => e.Department.Name == DeptName).ToListAsync();

        public async Task<IEnumerable<Employee>> Search(string name)
        => await context.Employees.Where(e => e.Name.Contains(name)).ToListAsync();

 
        //public int Add(Employee employee)
        //{
        //    context.Employees.Add(employee);
        //    return context.SaveChanges();
        //}

        //public int Delete(Employee employee)
        //{
        //    context.Employees.Remove(employee);
        //    return context.SaveChanges();
        //}

        //public Employee Get(int? id)
        //=> context.Employees.FirstOrDefault(x => x.Id == id);


        //public IEnumerable<Employee> GetAll()
        //=> context.Employees.ToList();

        //public int Update(Employee employee)
        //{
        //    context.Employees.Update(employee);
        //    return context.SaveChanges();
        //}
    }

}
