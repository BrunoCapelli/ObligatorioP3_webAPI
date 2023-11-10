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
        public List<EcosistemaAmenaza> GetByEcosistemaId(int id)
        {

            List<EcosistemaAmenaza> entity = _restContext.GetAll()
            return entity;
        }

    }
}
