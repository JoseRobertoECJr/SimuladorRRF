using SimuladorRRF.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimuladorRRF.Service
{
    public interface ISimuladorService
    {
        List<Process> GetProcesses();
        void SetProcesses(List<Process> newProcesses);
        List<Process> SimularProcessamento();
    }
}
