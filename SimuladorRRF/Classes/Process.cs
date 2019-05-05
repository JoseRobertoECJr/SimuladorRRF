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
        public int ProxChegada { get; set; }
        public int TempoCPU { get; set; }
        public int TempoExecutado {
            get {
                var temp = 0;
                foreach(var block in Blocks)
                {
                    if(block.Tipo == BlockTipoEnum.Processo)
                        temp++;
                }
                return temp;
            }
        }
        public int TurnAround { get => Blocks.Sum(block => block.Tempo); }
        public List<Block> Blocks;

        public Process()
        {
            Blocks = new List<Block>();
        }
    }
}
