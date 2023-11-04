using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Domain.DTO
{
    public class PaisDTO:IValidable
    {
        public int PaisId { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }

        public IEnumerable<EcosistemaMarinoDTO> Ecosistemas = new List<EcosistemaMarinoDTO>();
        public PaisDTO() { }
        public PaisDTO(Pais pais) {
            this.PaisId = pais.PaisId;
            this.Codigo = pais.Codigo;
            this.Nombre = pais.Nombre;
        }

        public void Validate() {
           if (this.PaisId == 0) {

           }

           if (this.Nombre == null || this.Nombre.Length < 2 || this.Nombre.Length > 50) {
                throw new NombreLargoException("El largo del nombre debe estar entre 2 y 50 caracteres");
           }
        }
    }
}
