using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class ElementoYaExisteException : Exception
    {
        public ElementoYaExisteException() { }
        public ElementoYaExisteException(string message) : base(message) { }
        public ElementoYaExisteException(string message, Exception innerException) : base(message, innerException) { }
    }
}
