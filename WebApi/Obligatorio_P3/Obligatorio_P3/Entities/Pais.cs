using Domain.DTO;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Pais : IValidable
    {
        public int PaisId { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }

        public List<EcosistemaMarino> ecosistemaMarinos = new List<EcosistemaMarino>();


        public Pais() { } 

        public Pais(PaisDTO paisDTO) {
            this.PaisId = paisDTO.PaisId;
            this.Nombre = paisDTO.Nombre;
            this.Codigo = paisDTO.Codigo;
        }

        public void Validate()
        {
            if (this.PaisId == 0)
            {

            }

            if (this.Nombre == null || this.Nombre.Length < 2 || this.Nombre.Length > 50)
            {
                throw new NombreLargoException("El largo del nombre debe estar entre 2 y 50 caracteres");
            }
        }
    }
}
