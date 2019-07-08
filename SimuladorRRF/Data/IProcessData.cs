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
        void LimpaPageTable(int oldProcessID);
        Page GetMemPrincipalFrame(int frame);
        int? GetOldestProcess(int processID);
        int SwapInSameProcess(Page page, int oldestPageNum);
        int SwapIn(Page page);
        void SwapOut(int oldProcessID);
        void AtualizaPageTable(Process process, int pageNum, int enderecoReal);
    }
}
