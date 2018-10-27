using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using CyberPushkin.addirionDLL;

namespace CyberPushkin.ngram
{
    public class Unigram : NGramStrategy
    {

        public NGramStrategy getNGramStrategy(String type)
        {
            switch (type)
            {
                case "unigram":
                   return new Unigram();
                //case "filtered_unigram":
                //    return new filteredunigram();
                //case "bigram":
                //    return new bigram(new unigram());
                //case "filtered_bigram":
                //    return new bigram(new filteredunigram());
                default:
                   return null;
            }
        }

        public ISet<String> getNGram(String verse)
        {
            if (verse == null)
            {
                verse = "";
            }

            // get all words and digits
            String[] words = Regex.Replace(verse.ToLower(), @"[\f\n\r\t\v]", " ").Split(' ');

            ISet<String> uniqueValues = new OrderedSet<String>();
            foreach (String word in words)
            {
                if (!word.Equals(""))
                {
                    uniqueValues.Add(word);
                }
            }
            return uniqueValues;
        }
    }
}
