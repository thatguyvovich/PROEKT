using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberPushkin.dao
{
    class EmptyRecordException : Exception
    {
        public EmptyRecordException(String s) : base(s) { }
    }
}
