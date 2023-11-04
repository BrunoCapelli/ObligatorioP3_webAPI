using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class NombreLargoException: Exception
    {
        public NombreLargoException() { }
        public NombreLargoException(string message): base(message) { }
        public NombreLargoException(string message, Exception innerException): base(message, innerException) { }
    }
}
