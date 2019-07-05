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
        public readonly int WSL = 4;
        public int?[] WorkingSet { get; set; }

        public PageTable(int numPags)
        {
            WorkingSet = new int?[WSL];
            TableRowArray = new TableRow[numPags];
        }

        public int QntInMem
        {
            get
            {
                var i = 0;
                foreach (var pageNum in WorkingSet)
                {
                    if (pageNum != null)
                        i++;
                }
                return i;
            }
        }

    }
}
