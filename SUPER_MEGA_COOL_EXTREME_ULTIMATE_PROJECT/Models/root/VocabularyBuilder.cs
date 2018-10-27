using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CyberPushkin.ngram;
using CyberPushkin.model;

namespace CyberPushkin.root
{
    class VocabularyBuilder
    {
        private readonly NGramStrategy nGramStrategy;

  VocabularyBuilder(NGramStrategy nGramStrategy)
        {
            if (nGramStrategy == null)
            {
                throw new ArgumentException();
            }

            this.nGramStrategy = nGramStrategy;
        }

        List<VocabularyWord> getVocabulary(List<Verse> verses)
        {
            if (verses == null ||
                verses.Count == 0)
            {
                throw new ArgumentException();
            }

            Dictionary<String, Int32> uniqueValues = new Dictionary<string, Int32>();
            List<VocabularyWord> vocabulary = new List<VocabularyWord>();

            // count frequency of use each word (converted to n-gram) from all Classifiable Texts
            //

            foreach (Verse verse in verses)
            {
                foreach (String word in nGramStrategy.getNGram(verse.getText()))
                {
                    if (uniqueValues.ContainsKey(word))
                    {
                        // increase counter
                        uniqueValues[word] = uniqueValues[word] + 1;
                    }
                    else
                    {
                        // add new word
                        uniqueValues[word] = 1;
                    }
                }
            }

            // convert uniqueValues to Vocabulary, excluding infrequent
            //

            foreach (KeyValuePair<String, Int32> entry in uniqueValues)
            {
                if (entry.Value > 3)
                {
                    vocabulary.Add(new VocabularyWord(entry.Key));
                }
            }

            return vocabulary;
        }
    }
}
