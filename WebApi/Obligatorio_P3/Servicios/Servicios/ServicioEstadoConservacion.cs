using Data_Access.IRepositorios;
using Domain.DTO;
using Domain.Entities;
using Servicios.IServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Servicios
{
    public class ServicioEstadoConservacion : IServicioEstadoConservacion
    {
        private IRepositorioEstadoConservacion _repoEstadoConservacion;
        public ServicioEstadoConservacion(IRepositorioEstadoConservacion repoEstadoConservacion) 
        {
            _repoEstadoConservacion = repoEstadoConservacion;
        }

        public EstadoConservacionDTO Add(EstadoConservacionDTO entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EstadoConservacionDTO> GetAll()
        {
            List<EstadoConservacionDTO> res = new List<EstadoConservacionDTO>();
            IEnumerable<EstadoConservacion> estadosGet = _repoEstadoConservacion.GetAll();
            foreach (EstadoConservacion p in estadosGet)
            {
                EstadoConservacionDTO estadoDTO = new EstadoConservacionDTO(p);
                res.Add(estadoDTO);
            }
            return res;
        }

        public void Update(EstadoConservacionDTO entity)
        {
            throw new NotImplementedException();
        }

        public EstadoConservacionDTO GetEstado(int estado)
        {
            EstadoConservacion estadoBuscado = new EstadoConservacion();
            estadoBuscado = _repoEstadoConservacion.GetEstado(estado);
            EstadoConservacionDTO  estadoResultado = new EstadoConservacionDTO(estadoBuscado);
            return estadoResultado;
        }

        public void Remove(int id) {
            throw new NotImplementedException();
        }
    }
}
