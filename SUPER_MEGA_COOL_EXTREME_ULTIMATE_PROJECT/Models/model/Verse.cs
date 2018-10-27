using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberPushkin.model
{
    public class Verse
    {

        private readonly String text;
        private readonly Dictionary<Position, PositionValue> positon;

        public Verse(String text, Dictionary<Position, PositionValue> positon)
        {
            this.text = text;
            this.positon = positon;
        }

        public Verse(String text)
        {
            this.text = text;
            this.positon = null;
        }

        public String getText()
        {
            return text;
        }

        public Dictionary<Position, PositionValue> getCharacteristics()
        {
            return positon;
        }

        public PositionValue getCharacteristicValue(Position characteristic)
        {
            return positon[characteristic];
        }

    }
}
