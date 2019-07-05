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

        PageTable GetPageTable(Process process);
        void LimpaPageTable(Process oldProcess);
        Page GetMemPrincipalFrame(int frame);
        int? GetOldestProcess();
        int SwapInSameProcess(Page page, int oldestPageNum);
        int SwapIn(Page page);
        void SwapOut(Process oldProcess);
        void AtualizaPageTable(Process process, int pageNum, int enderecoReal);
    }
}
