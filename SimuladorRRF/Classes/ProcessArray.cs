using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimuladorRRF.Classes
{
    public class ProcessArray
    {
        public Process[] Value { get; private set; }
        private int _max = 1000;

        public ProcessArray()
        {
            Value = new Process[_max];
        }

        public ProcessArray(Process[] processes)
        {
            Value = new Process[_max];
            for (var i = 0; i < _max; i++)
            {
                if (processes != null)
                    Value[i] = processes[i];
            }
        }

        public void Add(Process v)
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

        public void Remove(Process v)
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

        public Process Last()
        {

            Process v = new Process();
            for (var i = 0; i < _max; i++)
            {
                if (Value[i] != null)
                    v = Value[i];
            }

            return v;
        }


    }
}
