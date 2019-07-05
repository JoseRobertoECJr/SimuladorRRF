using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimuladorRRF.Classes
{
    public class OldProcessNFrame
    {
        public int Frame { get; set; }
        public int? OldProcessID { get; set; }

        public OldProcessNFrame(int? oldProcess, int frame)
        {
            OldProcessID = oldProcess;
            Frame = frame;
        }
    }
}
