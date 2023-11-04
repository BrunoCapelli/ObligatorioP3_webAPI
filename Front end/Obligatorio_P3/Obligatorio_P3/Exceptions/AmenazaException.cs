using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class AmenazaException : Exception
    {
        public AmenazaException() { }
        public AmenazaException(string message) : base(message) { }
        public AmenazaException(string message, Exception innerException) : base(message, innerException) { }
    }
}
