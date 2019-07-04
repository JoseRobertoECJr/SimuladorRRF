using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using static SimuladorRRF.Utils;

namespace SimuladorRRF.Classes
{
    public class Page
    {
        public int ProcessID { get; set; }
        public int PageNum { get; set; }

        public Page(Process process, int pageNum)
        {
            ProcessID = (int)process.Id;
            PageNum = pageNum;
        }
    }
}
