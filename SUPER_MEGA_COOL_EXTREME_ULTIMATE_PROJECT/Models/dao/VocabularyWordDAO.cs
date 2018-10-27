using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CyberPushkin.model;

namespace CyberPushkin.dao
{
    public interface VocabularyWordDAO
    {
        List<VocabularyWord> getAll();

        void addAll(List<VocabularyWord> vocabulary) ;
    }
}
