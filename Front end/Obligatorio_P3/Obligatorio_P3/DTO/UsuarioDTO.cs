using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class UsuarioDTO:IValidable
    {
        public int UsuarioDTOId { get; set; }
        public string Alias { get; set; }
        public string Password { get; set; }
        public DateTime FechaAlta { get; set; }
        public UsuarioDTO() { }

        public void Validate()
        {
        
            if (Alias.Length < 6)
            {
                throw new StringException("El Alias debe tener al menos 6 caracteres.");
            }
            if (Password.Length < 8)
            {
                throw new StringException("La contraseña debe tener al menos 8 caracteres");
            }
            if (!Password.Contains('.') && !Password.Contains(',') && !Password.Contains('#') && !Password.Contains(';') &&
                !Password.Contains(':') && !Password.Contains('!'))
            {
                throw new StringException("La constraseña debe tener al menos uno de los siguientes: . , # ; : ! ");
            }
            if (!tieneUnaMayus(Password) || !tieneUnaMinus(Password) )
            {
                throw new StringException("La contraseña debe tener al menos una mayuscula y una minuscula");
            }
            if (!tieneDigito(Password))
            {
                throw new StringException("La contraseña debe tener al menos un numero");
            }
            
        }

        public bool tieneUnaMayus(string unString)
        {
            int contador = 0;
            bool result = false;

            foreach (char caracter in unString)
            {
                if (Char.IsUpper(caracter))
                {
                    contador++;
                }
            }

            if (contador >= 1)
            {
                result = true;
            }
            return result;
        }
        public bool tieneUnaMinus(string unString)
        {
            int contador = 0;
            bool result = false;

            foreach (char caracter in unString)
            {
                if (Char.IsLower(caracter))
                {
                    contador++;
                }
            }

            if (contador >= 1)
            {
                result = true;
            }
            return result;
        }

        public bool tieneDigito(string unString)
        {
            int contador = 0;
            bool result = false;

            foreach (char caracter in unString)
            {
                if (Char.IsDigit(caracter))
                {
                    contador++;
                }
            }

            if (contador >= 1)
            {
                result = true;
            }
            return result;

        }

        public UsuarioDTO(Usuario user)
        {
            this.Alias = user.Alias;
            this.Password = user.Password;
            this.FechaAlta = user.FechaAlta;
        }
    }
}
