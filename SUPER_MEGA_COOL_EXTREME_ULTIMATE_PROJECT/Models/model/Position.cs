using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CyberPushkin.addirionDLL;

namespace CyberPushkin.model
{
    public class Position
    {


        private readonly String name;
        private int id;
        private ISet<PositionValue> possibleValues;

        private Position(int id, String name, ISet<PositionValue> possibleValues)
        {
            this.id = id;
            this.name = name;
            this.possibleValues = possibleValues;
        }

        public Position(int id, String name)
        {
            this.id = id;
            this.name = name;
            this.possibleValues = new OrderedSet<PositionValue>();
        }

        public Position(String name, ISet<PositionValue> possibleValues)
        {
            this.id = 0;
            this.name = name;
            this.possibleValues = possibleValues;
        }

        public Position(String name)
        {
            this.id = 0;
            this.name = name;
            this.possibleValues = new OrderedSet < PositionValue >();
        }

        public int getId()
        {
            return id;
        }

        public void setId(int id)
        {
            this.id = id;
        }

        public String getName()
        {
            return name;
        }

        public ISet<PositionValue> getPossibleValues()
        {
            return possibleValues;
        }

        public void setPossibleValues(ISet<PositionValue> possibleValues)
        {
            this.possibleValues = possibleValues;
        }

        public void addPossibleValue(PositionValue value)
        {
            possibleValues.Add(value);
        }

        
 

    }
}
