using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Services.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly GuanaHospiContext _context;
        private DbSet<T> entities;

        public GenericRepository(GuanaHospiContext context)
        {
            _context = context;
            entities = context.Set<T>();

        }

        public IEnumerable<T> GetAll()
        {
            return entities.ToList();
        }

        public T GetById(object id)
        {
            return entities.Find(id);
        }

        public void Insert(T obj)
        {
            entities.Add(obj);
        }

        public void Update(T obj)
        {
            entities.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
        public void Delete(object id)
        {
            T existing = entities.Find(id);
            entities.Remove(existing);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
