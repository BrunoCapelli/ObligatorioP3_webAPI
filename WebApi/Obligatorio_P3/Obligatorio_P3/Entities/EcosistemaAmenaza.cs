using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities {
    public class EcosistemaAmenaza {
        public int EcosistemaMarinoId { get; set; }
        public EcosistemaMarino ecosistemaMarino { get; set; }
        public int AmenazaId { get; set; }
        public Amenaza amenaza { get; set; }

        public EcosistemaAmenaza() { }
        public EcosistemaAmenaza(EcosistemaMarino Ecosistema, Amenaza Amenaza) 
        {
            this.EcosistemaMarinoId = Ecosistema.EcosistemaMarinoId;
            this.ecosistemaMarino = Ecosistema;

            this.AmenazaId = Amenaza.AmenazaId;
            this.amenaza = Amenaza;
        }

    }
}
