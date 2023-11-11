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
    public class RepositorioEstadoConservacion: IRepositorioEstadoConservacion
    {

        private IRestContext<EstadoConservacion> _restContext;

        public RepositorioEstadoConservacion(IRestContext<EstadoConservacion> restContext) { 
            _restContext = restContext;
        }

        public EstadoConservacion Add(EstadoConservacion entity) {
            throw new NotImplementedException();
        }

        public IEnumerable<EstadoConservacion> GetAll()
        {
            string filters = "";
            IEnumerable<EstadoConservacion> entity = _restContext.GetAll(filters).GetAwaiter().GetResult();
            return entity;
        }

        public EstadoConservacion GetEstado(int estado)
        {
            EstadoConservacion Estado = _restContext.GetById(estado).GetAwaiter().GetResult();
            return Estado;
        }

        public void Remove(EstadoConservacion entity) {
            throw new NotImplementedException();
        }

        public void Update(EstadoConservacion entity) {
            throw new NotImplementedException();
        }
    }
}
