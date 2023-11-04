using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO {
    public class EcosistemaMarinoDTO:IValidable {
        public int EcosistemaMarinoId { get; set; }
        public string Nombre { get; set; }
        public UbiGeografica UbicacionGeografica { get; set; }
        public double Area { get; set; }
        public List<EspecieDTO> Especies = new List<EspecieDTO>();
        public List<AmenazaDTO> Amenazas = new List<AmenazaDTO>();
        public EstadoConservacionDTO EstadoConservacion { get; set; }
        public int PaisId { get; set; }
        public string PaisNombre { get; set; }
        public string ImagenURL { get; set; }
        public int NombreMin { get; set; }
        public int NombreMax { get; set; }

        public IFormFile Imagen { get; set; }


        public EcosistemaMarinoDTO() { }

        public EcosistemaMarinoDTO(EcosistemaMarino eco) {
            this.EcosistemaMarinoId = eco.EcosistemaMarinoId;
            this.Nombre = eco.Nombre;
            this.Area = eco.Area;
            this.UbicacionGeografica = eco.UbicacionGeografica;
            this.PaisId = eco.PaisId;
            this.EstadoConservacion = new EstadoConservacionDTO(eco.EstadoConservacion);
        }

        public EcosistemaMarinoDTO(EcosistemaMarino eco, EstadoConservacionDTO EC)
        {
            this.EcosistemaMarinoId = eco.EcosistemaMarinoId;
            this.Nombre = eco.Nombre;
            this.Area = eco.Area;
            this.UbicacionGeografica = eco.UbicacionGeografica;
            this.PaisId = eco.PaisId;
            this.EstadoConservacion = EC;
        }

        public EcosistemaMarinoDTO(string Nombre, UbiGeografica UbicacionGeografica, double Area, EstadoConservacionDTO estadoConservacion,int PaisId) {
            //this.EcosistemaMarinoId = EcosistemaMarinoId;
            this.Nombre = Nombre;
            this.UbicacionGeografica = UbicacionGeografica;
            this.Area = Area;
            this.EstadoConservacion = estadoConservacion;
            this.PaisId = PaisId;
        }

        public void Validate() {
            if (this.Nombre.Length < NombreMin || this.Nombre.Length> NombreMax) {
                throw new NombreLargoException("El largo del nombre debe estar entre 2 y 50 caracteres");
            }
            if (Area <= 0) {
                throw new RangoException("El area debe ser positiva");
            }

        }
    }
}
