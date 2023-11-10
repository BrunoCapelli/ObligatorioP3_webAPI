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
    public class RepositorioEcosistemaMarino<EcosistemaMarino> :  IRepositorioEcosistemaMarino<EcosistemaMarino>
    {

        private IRestContext<EcosistemaMarino> _restContext;

        public RepositorioEcosistemaMarino(IRestContext<EcosistemaMarino> restContext)
        {
            _restContext = restContext;
        }

        public IEnumerable<EcosistemaMarino> GetById(int id)
        {
            string filters = "?" + id;
            IEnumerable<EcosistemaMarino> entity = _restContext.GetAll(filters).GetAwaiter().GetResult();
            return entity;
        }

        public IEnumerable<EcosistemaMarino> GetEcosistemaByName(string nombre) {
            string filters = "?" + nombre;
            IEnumerable<EcosistemaMarino> entity = _restContext.GetAll(filters).GetAwaiter().GetResult();
            return entity;
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
