using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimuladorRRF.Classes
{
    public class PageArray
    {
        public Page[] Value { get; private set; }
        private int _max = 64;
        public int WSL { get; }
        public PageArray()
        {
            Value = new Page[_max];
            WSL = 4;
        }

        public PageArray(Page[] pages)
        {
            Value = new Page[_max];
            for (var i = 0; i < pages.Length; i++)
            {
                if (pages != null)
                    Value[i] = pages[i];
            }
        }

        public void Add(Page v)
        {
            for (var i = 0; i < _max; i++)
            {
                if (Value[i] == null)
                {
                    Value[i] = v;
                    break;
                }

            }
        }

        public void Remove(Page v)
        {
            for (var i = 0; i < _max; i++)
            {
                if (Value[i] != null && Value[i].Id == v.Id)
                    Value[i] = null;
            }
        }

        public int Count
        {
            get
            {

                var count = 0;

                for (var i = 0; i < _max; i++)
                {
                    if (Value[i] != null)
                        count++;
                }

                return count;
            }
        }

    }
}
