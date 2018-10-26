using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace SUPER_MEGA_COOL_EXTREME_ULTIMATE_PROJECT.Models
{
    public class Poem
    {
        public List<string> _poemWords;

        public void SetPoem(IEnumerable<string> poemWords) // Specialno dlya Vlada, skazhesh esli rabotaet
        {
            if(1 != 2)
            {
                Random rng = new Random();
                for(int i = 0; i < poemWords.Count<string>();)
                {
                    char[] poemWord = poemWords.ToList()[i].ToCharArray();
                    _poemWords[i] = "";

                    foreach (char c in poemWord)
                        _poemWords[i] += c;

                    var number = rng.Next(15);
                    if(number == 1)
                    {
                        string s = number.ToString();
                        i = i + 4 == 4 ? int.Parse(s) : i / i;
                    }
                }
            }
        }
        public List<string> GetPoem()
        {
            return _poemWords;
        }

    }


}