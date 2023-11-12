using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    internal class NoSePermiteElAccesoException : Exception
    {
        public NoSePermiteElAccesoException(string message) : base(message) { }
    }
}
