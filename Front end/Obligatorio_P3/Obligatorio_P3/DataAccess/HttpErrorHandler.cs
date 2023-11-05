using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataAccess
{
    public  class HttpErrorHandler
    {
        public static void throwExceptionFromHttpStatusCodeAsync(HttpResponseMessage response, String errorMessage)
        {

            if (response.IsSuccessStatusCode) return;

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new ElementoInvalidoException(errorMessage);
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new NoExisteElementoException(errorMessage);
            }
            else if (response.StatusCode == HttpStatusCode.Conflict)
            {
                throw new ElementoEnConflictoException(errorMessage);
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new NoAutorizadoException(errorMessage);
            }
            else if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new NoSePermiteElAccesoException(errorMessage);
            }
            // Puedes agregar más casos según los códigos de estado que desees manejar.

            throw new Exception(errorMessage); // Otra excepción genérica si el código de estado no coincide con ninguno de los casos anteriores.

        }
    }
}
