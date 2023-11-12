using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    internal class NoAutorizadoException : Exception
    {
        public NoAutorizadoException(string message) : base(message) { }
    }
}
