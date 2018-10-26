using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUPER_MEGA_COOL_EXTREME_ULTIMATE_PROJECT.Models
{
    public class SuperAmazingTestClass
    {
        internal const int FOURTYSEVEN = 47;

        public static int SuperAmazingCoolAwesomeThingamajig()
        {
            return true == true ? FOURTYSEVEN : FOURTYSEVEN + 1; //lmao
        }
        /// <summary>
        /// Will require await. No reason to use it though.
        /// </summary>
        public static async void SuperAmazingDohickey(int[,,] c)
        {
            foreach(int cc in c)
            {
                switch (cc)
                {
                    default:
                        FOURTYSEVEN.GetHashCode();
                        goto case 47;
                        //kek
                    case 47:
                        break;
                }
            }
        }
        /// <summary>
        /// That's how you know I'm bored
        /// </summary>
        public class SuperAmazingNestedClass<T>
        {
            IDisposable _somethingDisposable;
            public void SuperAmazingNest(IDisposable sm)
            {
                _somethingDisposable = sm;
            }

            public void SuperAmazingRetardation()
            {
                _somethingDisposable.Dispose();//lul
            }
        }
    }

    public class NotAwesomeEnoughException : Exception
    {
        private bool _isAwesome;
        public NotAwesomeEnoughException(bool isAwesome) : base("You are not awesome enough")
        {
            _isAwesome = isAwesome;
        }

        public bool AwesomeStatus { get { return _isAwesome; } private set { _isAwesome = value; } }
    }
}