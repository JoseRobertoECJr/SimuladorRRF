using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimuladorRRF.Classes
{
    public class PageTable
    {
        public TableRow[] row;
        public int[] WorkingSet;
        private int WSL = 4;

        public PageTable(int numPags)
        {
            WorkingSet = new int[WSL];
            row = new TableRow[numPags];
        }
    }
}
