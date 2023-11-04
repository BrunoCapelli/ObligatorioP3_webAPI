using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTO;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Domain.Entities
{
    public class EstadoConservacion : IValidable
    {
        public int EstadoConservacionId { get; set; }
        public string Nombre { get; set; }
        public int ValorDesde { get; set; }
        public int ValorHasta { get; set; }
        public EstadoConservacion() { }
        public EstadoConservacion(EstadoConservacionDTO estadoConservacionDTO) {
            this.EstadoConservacionId = estadoConservacionDTO.EstadoConservacionId;
            this.Nombre = estadoConservacionDTO.Nombre;
            this.ValorDesde = estadoConservacionDTO.ValorDesde;
            this.ValorHasta = estadoConservacionDTO.ValorHasta;
        }
        public void Validate()
        {
            if (!String.IsNullOrEmpty(this.Nombre))
            {
                throw new StringException("El nombre del estado no puede ser vacio");
            }
            if (ValorDesde <= 0 || ValorHasta <= 0)
            {
                throw new RangoException("Los valores ingresados no son correctos");
            }
        }
    }
}
