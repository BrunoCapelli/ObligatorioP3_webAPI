using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EcosistemaMarinoEspecie
    {
        
        public EcosistemaMarino EcosistemaMarino { get; set; }
        public int EcosistemaMarinoId;

        public Especie Especie { get; set; }
        public int EspecieId { get; set; }

        public EcosistemaMarinoEspecie() { }

        public EcosistemaMarinoEspecie(EcosistemaMarino ecosistema, Especie especie)
        {
            this.EcosistemaMarinoId = ecosistema.EcosistemaMarinoId;
            this.EcosistemaMarino = ecosistema;

            this.EspecieId = especie.EspecieId;
            this.Especie = especie;
        }
    }
}
