using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services.Exceptions
{
    public class Under18Exception : Exception
    {
        public Under18Exception(string? message) : base(message)
        {
        }
    }
}
