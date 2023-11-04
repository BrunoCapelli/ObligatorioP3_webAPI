using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class RangoException: Exception
    {
        public RangoException() { }
        public RangoException(string message): base(message) { }
        public RangoException(string message, Exception innerException): base(message, innerException) { }
    }
}
