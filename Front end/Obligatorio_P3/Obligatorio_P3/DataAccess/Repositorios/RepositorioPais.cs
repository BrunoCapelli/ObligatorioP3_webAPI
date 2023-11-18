using Data_Access.IRepositorios;
using Domain.DataAccess;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositorios
{
    public class RepositorioPais : IRepositorioPais {

        private IRestContext<Pais> _restContext;

        public RepositorioPais(IRestContext<Pais> restContext) {
            _restContext = restContext;
        }

        public Pais Add(Pais entity, string token) {
            throw new NotImplementedException();
        }

        public IEnumerable<Pais> GetAll() {
            string filters = "";
            IEnumerable<Pais> paises = _restContext.GetAll(filters).GetAwaiter().GetResult();
            return paises;

        }

        public Pais GetPais(int id) {

            Pais p = _restContext.GetById(id).GetAwaiter().GetResult();

            return p;

        }

        public void Remove(Pais entity, string token) {
            throw new NotImplementedException();
        }

        public void Update(Pais entity) {
            throw new NotImplementedException();
        }
    }
}
