using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimuladorRRF.Classes
{
    public class PageTableArray
    {
        public PageTable[] Value { get; set; }

        public PageTableArray(int maxProcesses)
        {
            Value = new PageTable[maxProcesses];
        }


    }
}
