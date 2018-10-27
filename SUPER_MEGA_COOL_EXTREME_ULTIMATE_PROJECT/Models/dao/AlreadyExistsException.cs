using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberPushkin.dao
{
    class AlreadyExistsException  : Exception
    {
        public AlreadyExistsException(String s) : base(s) { }
    }
}
