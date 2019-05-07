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
        public int TempoExecutado {
            get {
                var tempEx = 0;
                foreach(var block in Blocks)
                {
                    if(block.Tipo == BlockTipoEnum.Processo)
                        tempEx += block.Tempo;
                }
                return tempEx;
            }
        }
        public int TurnAround {
            get {
                Blocks.Sum(block => block.Tempo);

                var tempTrAr = 0;
                foreach(var block in Blocks)
                {
                    if(block.Tipo != BlockTipoEnum.NonExec)
                        tempTrAr += block.Tempo;
                }

                return tempTrAr;
            }
        }
        public int TempoTotal { get => Chegada + TurnAround; }

        public List<Block> Blocks;

        public Process()
        {
            Blocks = new List<Block>();
        }

        public Process(Process process)
        {
            Id = process.Id;
            Chegada = process.Chegada;
            TempoCPU = process.TempoCPU;
            Blocks = new List<Block>(process.Blocks);
        }

    }
}
