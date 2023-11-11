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
    public class RepositorioEspecieAmenaza: IRepositorioEspecieAmenaza
    {

        private IRestContext<EspecieAmenaza> _restContext;

        public RepositorioEspecieAmenaza(IRestContext<EspecieAmenaza> restContext)
        {
            _restContext = restContext;
        }

        public EspecieAmenaza Add(EspecieAmenaza entity) {
            throw new NotImplementedException();
        }

        public IEnumerable<EspecieAmenaza> GetAll() {
            throw new NotImplementedException();
        }

        public IEnumerable<EspecieAmenaza> GetByEspecieId(int id)
        {
            string filters = "?id=" + id.ToString();
            IEnumerable<EspecieAmenaza> entity = _restContext.GetAll(filters).GetAwaiter().GetResult();
            return entity;
        }

        public void Remove(EspecieAmenaza entity) {
            throw new NotImplementedException();
        }

        public void Update(EspecieAmenaza entity) {
            throw new NotImplementedException();
        }
    }
}
