using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimuladorRRF.Classes
{
    public class OldProcessNFrame
    {
        public int Frame { get; set; }
        public Process OldProcess { get; set; }

        public OldProcessNFrame(Process oldProcess, int frame)
        {
            OldProcess = oldProcess;
            Frame = frame;
        }
    }
}
