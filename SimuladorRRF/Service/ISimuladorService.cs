using SimuladorRRF.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimuladorRRF.Service
{
    public interface ISimuladorService
    {
        ProcessArray GetProcesses();
        void SetProcesses(ProcessArray newProcesses);
        ProcessArray SimularProcessamento();
        void ChangeProcessListData(ProcessArray processList);
        void ChangeCycleLength(int cycleLength);
        int GetCycleLength();
    }
}
