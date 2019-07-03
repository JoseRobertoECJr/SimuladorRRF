using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimuladorRRF.Classes;

namespace SimuladorRRF.Data
{
    public interface IProcessData
    {
        ProcessArray GetProcesses();
        void SetProcesses(ProcessArray newProcesses);
        int GetCycleLength();
        void SetCycleLength(int newLength);
        int? GetPageTableFrameAddress(Process process, int pageNum);
        void LimpaPageTable(Process oldProcess);
        Page GetMemPrincipalFrame(int frame);
    }
}
