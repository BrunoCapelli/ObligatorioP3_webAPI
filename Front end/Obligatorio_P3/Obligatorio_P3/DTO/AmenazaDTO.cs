using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Domain.DTO {
    public class AmenazaDTO:IValidable {
        public int AmenazaId { get; set; }
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public int GradoPeligrosidad { get; set; }

        public AmenazaDTO() { }

        public AmenazaDTO(Amenaza amenaza) {
            this.AmenazaId = amenaza.AmenazaId;
            this.Nombre = amenaza.Nombre;
            this.GradoPeligrosidad = amenaza.GradoPeligrosidad;
            this.Descripcion = amenaza.Descripcion;
        }

        public void Validate() {
            if (GradoPeligrosidad < 1 || GradoPeligrosidad > 10) {
                throw new RangoException("El grado de peligrosidad debe ser entre 1 y 10.");
            }
            if (Descripcion == "") {
                throw new StringException("La descripcion no puede ser vacio");
            }
            if (Descripcion.Length < 50 || Descripcion.Length > 500) {
                throw new NombreLargoException("La descripcion debe contener entre 50 y 500 caracteres");
            }
        }
    }
}
