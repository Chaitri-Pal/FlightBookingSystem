using FlightBookingSystem.DAL.Data;
using FlightBookingSystem.DAL.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.DAL.DataAccess
{
    public class Repo<T> : IRepo<T> where T : class
    {
        private readonly ApplicationDBContext _db;
        internal DbSet<T> DbSet;
        public Repo(ApplicationDBContext db) 
        { 
            _db = db;
            DbSet = db.Set<T>();
        }
        //Async function so that our mehods do not have to wait for anything
        public void AddAsync(T entity)
        {
            DbSet.AddAsync(entity);
        }

        //Async method always returns a task
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            IQueryable<T> query = DbSet;
            return await query.ToListAsync();
        }

        //Method calling Async method should also be Async
        public async Task<IEnumerable<T>> GetAllListAsync(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = DbSet;
            query = query.Where(filter);
            return await query.ToListAsync();
        }

        public async Task<T> GetFirstorDefaultAsync(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = DbSet;
            query = query.Where(filter);
            return await query.FirstOrDefaultAsync();
        }

        public void Remove(T entity)
        {
            DbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            DbSet.RemoveRange(entities);
        }

        public void UpdateExisting(T entity)
        {
            DbSet.Update(entity);
        }
    }
}
