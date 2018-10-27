using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberPushkin.observer
{
    public interface Observable
    {
        void addObserver(Observer o);

        void removeObserver(Observer o);

        void notifyObservers(String verse);
    }
}
