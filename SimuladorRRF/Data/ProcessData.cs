using SimuladorRRF.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimuladorRRF.Data
{
    public class ProcessData : IProcessData
    {
        private int CycleLength = 3;
        public int maxProcesses = 1000;
        public int qntProcesses = 0;
        private ProcessArray Processes;
        private PageArray[] PageTable;
        public ProcessData()
        {
            PageTable = new PageArray[maxProcesses];
            Processes = new ProcessArray();

            Processes.Add(new Process
            {
                Id = "A",
                Chegada = 2,
                TempoCPU = 15,
                Blocks = new BlockArray()
            });
        }

        public ProcessArray GetProcesses()
        {
            return Processes;
        }

        public void SetProcesses(ProcessArray newProcesses)
        {
            Processes = newProcesses;
        }

        public int GetCycleLength()
        {
            return CycleLength;
        }

        public void SetCycleLength(int newLength)
        {
            CycleLength = newLength;
        }

    }
}
