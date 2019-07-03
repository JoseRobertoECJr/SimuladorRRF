using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimuladorRRF.Classes
{
    public class PageTable
    {
        public int ProcessID { get; set; }
        public TableRow[] TableRowArray { get; set; }
        

        public PageTable(int numPags)
        {
            TableRowArray = new TableRow[numPags];
        }
    }
}
