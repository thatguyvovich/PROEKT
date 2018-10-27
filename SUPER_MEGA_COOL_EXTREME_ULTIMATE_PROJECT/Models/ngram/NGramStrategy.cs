using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberPushkin.ngram
{
    public interface NGramStrategy
    {
        NGramStrategy getNGramStrategy(String type);
        ISet<String> getNGram(String verse);
    }
}
