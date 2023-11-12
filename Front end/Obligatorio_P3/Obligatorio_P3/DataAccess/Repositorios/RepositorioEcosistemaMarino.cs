using Data_Access.IRepositorios;
using Domain.DataAccess;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositorios
{
    public class RepositorioEcosistemaMarino:  IRepositorioEcosistemaMarino
    {

        private IRestContext<EcosistemaMarino> _restContext;

        public RepositorioEcosistemaMarino(IRestContext<EcosistemaMarino> restContext)
        {
            _restContext = restContext;
        }

        public EcosistemaMarino GetById(int id)
        {
            EcosistemaMarino eco = new EcosistemaMarino();
            string filters = "?" + id;
            IEnumerable<EcosistemaMarino> entity = _restContext.GetAll(filters).GetAwaiter().GetResult();
            foreach(var e in entity)
            {
                eco = e;
            }
            return eco;
        }

        public EcosistemaMarino GetEcosistemaByName(string nombre) {

            EcosistemaMarino eco = new EcosistemaMarino();
            string filters = "?name=" + nombre;
            IEnumerable<EcosistemaMarino> entity = _restContext.GetAll(filters).GetAwaiter().GetResult();
            foreach (var e in entity)
            {
                eco = e;
            }
            return eco;
        }

        public IEnumerable<EcosistemaMarino > GetAllEcosistemas() {
            string filters = "?" ;
            IEnumerable<EcosistemaMarino> entity = _restContext.GetAll(filters).GetAwaiter().GetResult();
            return entity;
        }

        public EcosistemaMarino Add(EcosistemaMarino entity)
        {
            return _restContext.Add(entity).GetAwaiter().GetResult();
        }

        public void Update(EcosistemaMarino entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(EcosistemaMarino entity)
        {
            _restContext.Remove(entity).GetAwaiter().GetResult();
        }

        public IEnumerable<EcosistemaMarino> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
