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

        private ProcessArray Processes;

        

        public ProcessData()
        {
            Processes = new ProcessArray();

            Processes.Add(new Process
            {
                Id = "A",
                Chegada = 2,
                TempoCPU = 15,
                Blocks = new BlockArray()
            });
            
            Processes.Add(new Process
            {
                Id = "B",
                Chegada = 3,
                TempoCPU = 8,
                Blocks = new BlockArray()
            });
            Processes.Add(new Process
            {
                Id = "C",
                Chegada = 4,
                TempoCPU = 12,
                Blocks = new BlockArray()
            });
            Processes.Add(new Process
            {
                Id = "D",
                Chegada = 5,
                TempoCPU = 9,
                Blocks = new BlockArray()
            });
            Processes.Add(new Process
            {
                Id = "E",
                Chegada = 7,
                TempoCPU = 17,
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
