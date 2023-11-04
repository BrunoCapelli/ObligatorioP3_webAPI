using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities {
    public class EspecieAmenaza {
        public int EspecieId { get; set; }
        public Especie Especie { get; set; }
        public int AmenazaId { get; set; }
        public Amenaza Amenaza { get; set; }
        public EspecieAmenaza() { }
        public EspecieAmenaza(Amenaza amenaza, Especie especie) 
        {
            this.AmenazaId = amenaza.AmenazaId;
            this.Amenaza = amenaza;

            this.EspecieId = especie.EspecieId;
            this.Especie = especie;
        }
    }
}
