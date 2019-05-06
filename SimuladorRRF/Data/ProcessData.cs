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

        private List<Process> Processes = new List<Process>
        {

            new Process{
                Id = "A",
                ProxChegada = 2,
                TempoCPU = 15,
                Blocks = new List<Block>()
            },
            new Process{
                Id = "B",
                ProxChegada = 3,
                TempoCPU = 8,
                Blocks = new List<Block>()
            },
            new Process{
                Id = "C",
                ProxChegada = 4,
                TempoCPU = 12,
                Blocks = new List<Block>()
            },
            new Process{
                Id = "D",
                ProxChegada = 5,
                TempoCPU = 9,
                Blocks = new List<Block>()
            },
            new Process{
                Id = "E",
                ProxChegada = 7,
                TempoCPU = 17,
                Blocks = new List<Block>()
            },

        };

        public ProcessData()
        {

        }

        public List<Process> GetProcesses()
        {
            return Processes;
        }

        public void SetProcesses(List<Process> newProcesses)
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
