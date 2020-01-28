using HRMS.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HRMS.Persistance
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly HRMSContext db;
        private DbSet<T> dbSet;

        public Repository(HRMSContext context)
        {
            db = context;
            dbSet = db.Set<T>();
        }

        public List<T> GetAll()
        {
            return dbSet.ToList();
        }
        public T GetById(object id)
        {
            return dbSet.Find(id);
        }
        public void Insert(T obj)
        {
            dbSet.Add(obj);
        }
        public void Update(T obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }
        public void Delete(object id)
        {
            T getObjById = dbSet.Find(id);
            dbSet.Remove(getObjById);
        }
        public void Save()
        {
            db.SaveChanges();
        }
        
        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return this.dbSet.Any(predicate);
        }
        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return this.dbSet.Where(predicate);
        }
    }
}
