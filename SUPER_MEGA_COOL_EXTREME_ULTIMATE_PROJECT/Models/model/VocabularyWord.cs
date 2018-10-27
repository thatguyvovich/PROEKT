using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberPushkin.model 
{
    public class VocabularyWord 
    {
        private readonly int id;
        private readonly String value;

  public VocabularyWord(int id, String value)
        {
            this.id = id;
            this.value = value;
        }

        public VocabularyWord(String value)
        {
            this.id = 0;
            this.value = value;
        }

        public int getId()
        {
            return id;
        }

        public String getValue()
        {
            return value;
        }

    }
}
