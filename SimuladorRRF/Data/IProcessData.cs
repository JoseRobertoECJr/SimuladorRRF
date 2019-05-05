using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimuladorRRF.Classes;

namespace SimuladorRRF.Data
{
    public interface IProcessData
    {
        List<Process> GetProcesses();
        void SetProcesses(List<Process> newProcesses);
        int GetCycleLength();
        void SetCycleLength(int newLength);
    }
}
