using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberPushkin.model
{
    public class PositionValue
    {
        private readonly String value;
  private int id;

        public PositionValue(int id, String value)
        {
            this.id = id;
            this.value = value;
        }

        public PositionValue(String value)
        {
            this.id = 0;
            this.value = value;
        }

        public int getId()
        {
            return id;
        }

        public void setId(int id)
        {
            this.id = id;
        }

        public String getValue()
        {
            return value;
        }
    }
}
