using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class ElementoNoValidoException : Exception
    {
        public ElementoNoValidoException() { }
        public ElementoNoValidoException(string message) : base(message) { }
        public ElementoNoValidoException(string message, Exception innerException) : base(message, innerException) { }
    }
    
}
