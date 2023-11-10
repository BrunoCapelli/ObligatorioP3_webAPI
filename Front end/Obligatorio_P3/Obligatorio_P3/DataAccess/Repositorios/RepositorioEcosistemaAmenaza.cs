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
    public class RepositorioEcosistemaAmenaza<EcosistemaAmenaza> : IRepositorioEcosistemaAmenaza<EcosistemaAmenaza>
    {
        private IRestContext<EcosistemaAmenaza> _restContext;
        public RepositorioEcosistemaAmenaza(IRestContext<EcosistemaAmenaza> restContext)
        {
            _restContext = restContext;
        }
        public EcosistemaAmenaza Add(EcosistemaAmenaza entity)
        {
            _restContext.Add(entity).GetAwaiter().GetResult();
            return entity;
        }

        public IEnumerable<EcosistemaAmenaza> GetAll()
        {
            string filters = "";
            IEnumerable<EcosistemaAmenaza> entity = _restContext.GetAll(filters).GetAwaiter().GetResult();
            return entity;
        }

        public IEnumerable<EcosistemaAmenaza> GetByEcosistemaId(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(EcosistemaAmenaza entity)
        {
            throw new NotImplementedException();
        }

        public void Update(EcosistemaAmenaza entity)
        {
            throw new NotImplementedException();
        }

        List<EcosistemaAmenaza> IRepositorioEcosistemaAmenaza<EcosistemaAmenaza>.GetByEcosistemaId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
