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
    public class Amenaza:IValidable
    {
        public int AmenazaId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        
        public int GradoPeligrosidad { get; set; }

        List<EspecieAmenaza> especieAmenazas = new List<EspecieAmenaza>();
        List<EcosistemaAmenaza> ecosistemaAmenazas = new List<EcosistemaAmenaza> { };

        public Amenaza() { }
        public Amenaza(AmenazaDTO amenazaDTO) {
            this.AmenazaId = amenazaDTO.AmenazaId;
            this.Nombre = amenazaDTO.Nombre;
            this.GradoPeligrosidad = amenazaDTO.GradoPeligrosidad;
            this.Descripcion = amenazaDTO.Descripcion;
        }

        public void Validate()
        {
            if (GradoPeligrosidad < 1 || GradoPeligrosidad > 10)
            {
                throw new RangoException("El grado de peligrosidad debe ser entre 1 y 10.");
            }
            if (Descripcion == "")
            {
                throw new StringException("La descripcion no puede ser vacio");
            }
            if (Descripcion.Length < 50 || Descripcion.Length > 500)
            {
                throw new NombreLargoException("La descripcion debe contener entre 50 y 500 caracteres");
            }
        }

    }
}
