using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberPushkin.dao
{
    class NotExistsException : Exception
    {
        public NotExistsException(String s) : base(s) { }
    }
}
