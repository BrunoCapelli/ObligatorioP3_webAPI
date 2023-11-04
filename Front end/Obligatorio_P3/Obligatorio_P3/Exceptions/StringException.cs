using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class StringException: Exception
    {
        public StringException() { }
        public StringException(string message): base(message) { }
        public StringException(string message, Exception innerException): base(message, innerException) { }
    }
}
