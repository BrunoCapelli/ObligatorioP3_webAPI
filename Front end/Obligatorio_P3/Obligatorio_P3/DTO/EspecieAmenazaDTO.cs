using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class EspecieAmenazaDTO
    {
        public int EspecieId { get; set; }
        public Especie Especie { get; set; }
        public int AmenazaId { get; set; }
        public Amenaza Amenaza { get; set; }
        public EspecieAmenazaDTO() { }
        public EspecieAmenazaDTO(EspecieAmenaza especieAmenaza)
        {
            this.AmenazaId = especieAmenaza.AmenazaId;
            this.Amenaza = especieAmenaza.Amenaza;

            this.EspecieId = especieAmenaza.EspecieId;
            this.Especie = especieAmenaza.Especie;
        }
    }
}
