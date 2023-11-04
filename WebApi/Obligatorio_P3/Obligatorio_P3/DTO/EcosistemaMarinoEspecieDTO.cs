using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class EcosistemaMarinoEspecieDTO
    {
        public EcosistemaMarino EcosistemaMarino { get; set; }
        public int EcosistemaMarinoId;

        public Especie Especie { get; set; }
        public int EspecieId { get; set; }

        public EcosistemaMarinoEspecieDTO() { }

        public EcosistemaMarinoEspecieDTO(EcosistemaMarinoEspecie ecosistemaEspecie)
        {
            this.EcosistemaMarinoId = ecosistemaEspecie.EcosistemaMarinoId;
            this.EcosistemaMarino = ecosistemaEspecie.EcosistemaMarino;

            this.EspecieId = ecosistemaEspecie.EspecieId;
            this.Especie = ecosistemaEspecie.Especie;
        }
    }
}
