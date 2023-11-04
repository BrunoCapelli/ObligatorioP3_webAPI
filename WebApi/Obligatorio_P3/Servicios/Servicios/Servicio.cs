using Data_Access;
using Data_Access.IRepositorios;
using Microsoft.EntityFrameworkCore;
using Servicios.IServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Servicios
{
    public class Servicio<T>: IServicio<T> where T: class
    {
        private IRepositorio<T> _repo { get; set; }

        public Servicio(IRepositorio<T> repo) // Creo que esto no corresponde. Posible Issue
        {
            _repo = repo;
        }

        public T Add(T entity)
        {
            return _repo.Add(entity);
        }

        public void Update(T entity)
        {
            _repo.Update(entity);
        }

        public void Remove(int id)
        {
            //_repo.Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _repo.GetAll();
        }


    }
}
