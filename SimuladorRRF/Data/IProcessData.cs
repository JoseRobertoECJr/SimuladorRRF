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
        Process GetOldestProcess();
        int SwapInSameProcess(Process process);
        int SwapIn(Page page);
        void SwapOut(Process oldProcess);
    }
}
