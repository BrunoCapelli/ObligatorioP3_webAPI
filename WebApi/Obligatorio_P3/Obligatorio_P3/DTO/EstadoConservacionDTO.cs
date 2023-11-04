using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Domain.DTO {
    public class EstadoConservacionDTO : IValidable
    {
        public int EstadoConservacionId { get; set; }
        public string Nombre { get; set; }
        public int ValorDesde { get; set; }
        public int ValorHasta { get; set; }
        public EstadoConservacionDTO() { }
        public EstadoConservacionDTO(EstadoConservacion estadoConservacion) {
            this.EstadoConservacionId = estadoConservacion.EstadoConservacionId;
            this.Nombre = estadoConservacion.Nombre;
            this.ValorDesde = estadoConservacion.ValorDesde;
            this.ValorHasta = estadoConservacion.ValorHasta;

        }
        public EstadoConservacionDTO(int valor) 
        {
            if (valor > 100 && valor < 0)
            {
                throw new RangoException("Valor fuera de rango");
            }
            else
            {
                if (valor < 60)
                {
                    this.Nombre = "Malo";
                }
                if (valor >60 && valor < 70)
                {
                    this.Nombre = "Aceptable";
                }

                if(valor >= 70 && valor <= 95)
                {
                    this.Nombre = "Bueno";
                }

                if(valor > 95 && valor < 100)
                {
                    this.Nombre = "Optimo";
                }
            }
        }

        public void Validate()
        {
            if (!String.IsNullOrEmpty(this.Nombre))
            {
                throw new StringException("El nombre del estado no puede ser vacio");
            }
            if(ValorDesde <= 0 || ValorHasta <= 0)
            {
                throw new RangoException("Los valores ingresados no son correctos");
            }
        }
    }
    
}
