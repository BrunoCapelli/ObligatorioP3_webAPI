using Data_Access.IRepositorios;
using Microsoft.EntityFrameworkCore;

namespace Data_Access.Repositorios
{
    public class Repositorio<T> : IRepositorio<T> where T: class
    {
        //protected DbContext Context { get; set; }
        protected MiContexto Context { get; set; }
        public T Add(T entity)
        {
            Context.Set<T>().Add(entity);
            return entity;
        }


        public void Remove(T entity)
        {
            Context.Set<T>().Remove(entity);
        }


        public void Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }


        public void Save()
        {
            Context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return Context.Set<T>().ToList() ?? new List<T>();
        }


    }
}