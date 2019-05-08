using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using static SimuladorRRF.Utils;

namespace SimuladorRRF.Classes
{
    public class Process
    {
        public string Id { get; set; }
        public int Chegada { get; set; }
        public int TempoCPU { get; set; }
        public BlockArray Blocks;

        public int TempoExecutado
        {
            get
            {
                return Blocks.Count;
            }
        }
        public int TurnAround
        {
            get
            {
                return Blocks.TurnAround;
            }
        }
        public int TempoTotal
        {
            get
            {
                return Chegada + TurnAround;
            }
        }


        public Process()
        {
            Blocks = new BlockArray();
        }

        public Process(Process process)
        {
            Id = process.Id;
            Chegada = process.Chegada;
            TempoCPU = process.TempoCPU;
            Blocks = new BlockArray(process.Blocks);
        }

    }
}
