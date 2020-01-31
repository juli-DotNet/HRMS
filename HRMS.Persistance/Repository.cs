﻿using HRMS.Core.Model;
using HRMS.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HRMS.Persistance
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly HRMSContext db;
        private DbSet<T> dbSet;

        public Repository(HRMSContext context)
        {
            db = context;
            dbSet = db.Set<T>();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }
        public async Task<T> GetByIdAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }
        public async Task InsertAsync(T obj)
        {
            obj.CreatedOn = DateTime.Now;
            obj.IsValid = true;
             await dbSet.AddAsync(obj);
        }
        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return this.dbSet.Where(predicate);
        }
        public async Task<List<T>> WhereAsync(Expression<Func<T, bool>> predicate)
        {
            return await this.dbSet.Where(predicate).ToListAsync();
        }
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await this.dbSet.AnyAsync(predicate);
        }
        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
        public async Task DeleteAsync(object id)
        {
            T getObjById = await dbSet.FindAsync(id);
            dbSet.Remove(getObjById);
        }
        public Task UpdateAsync(T obj)
        {
            obj.ModifiedOn = DateTime.Now;
            obj.IsValid = true;
            db.Entry(obj).State = EntityState.Modified;
            return Task.CompletedTask;
        }
    }
}
