using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife 
{
    public class CustomLifeException : Exception
    {
        public CustomLifeException(string message) : base(message)
        {
        }
    }
}
