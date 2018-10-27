using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberPushkin.root
{
    public class EmptySheetException : Exception
    {
       public EmptySheetException(String s) : base(s)
        {      
                 
        }
    }
}
