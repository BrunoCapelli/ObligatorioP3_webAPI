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
    public class RepositorioEcosistemaAmenaza : IRepositorioEcosistemaAmenaza
    {
        private IRestContext<EcosistemaAmenaza> _restContext;
        public RepositorioEcosistemaAmenaza(IRestContext<EcosistemaAmenaza> restContext)
        {
            _restContext = restContext;
        }
        public EcosistemaAmenaza Add(EcosistemaAmenaza entity, string token)
        {
            _restContext.Add(entity, token).GetAwaiter().GetResult();
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
            string filters = "?ecosistemaID="+id;
            IEnumerable<EcosistemaAmenaza> entity = _restContext.GetAll(filters).GetAwaiter().GetResult();
            return entity;
        }

        public void Remove(EcosistemaAmenaza entity, string token)
        {
            throw new NotImplementedException();
        }

        public void Update(EcosistemaAmenaza entity)
        {
            throw new NotImplementedException();
        }


    }
}
