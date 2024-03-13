using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MVCAppDBContext context;

        public GenericRepository(MVCAppDBContext context) 
        {
            this.context = context;
        }
        public async Task<int> Add(T obj)
        {
            await context.Set<T>().AddAsync(obj);
            return await context.SaveChangesAsync();
        }

        public async Task<int> Delete(T obj)
        {
            context.Set<T>().Remove(obj);
            return await context.SaveChangesAsync();
        }

        public async Task<T> Get(int? id)
        => await context.Set<T>().FindAsync(id);

        public async Task<IEnumerable<T>> GetAll()
         => await context.Set<T>().ToListAsync();


        public async Task<int> Update(T obj)
        {
            context.Set<T>().Update(obj);
            return await context.SaveChangesAsync();
        }
    }
}
