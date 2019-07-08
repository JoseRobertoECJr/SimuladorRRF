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
        public int Id { get; set; }
        public int Chegada { get; set; }
        public int TempoCPU { get; set; }
        public BlockArray Blocks;

        public int TempoExecutado
        {
            get
            {
                return Blocks.TempoExecutado;
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

        public int NumPags { get; }
        

        public Process()
        {
            // Gera numeros de 4 a 8
            NumPags = (new Random()).Next(4, 9);
            Blocks = new BlockArray();
        }

        public Process(Process process)
        {
            Id = process.Id;
            Chegada = process.Chegada;
            TempoCPU = process.TempoCPU;
            Blocks = new BlockArray(process.Blocks);

            // Gera numeros de 4 a 8
            NumPags = (new Random()).Next(4, 9);
        }

        public int NextPage()
        {
            //prox pagina que o processo precisa para ser executado
            return (new Random()).Next(0, NumPags);
        }

    }
}
