using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SimuladorRRF.Utils;

namespace SimuladorRRF.Classes
{
    public class Block
    {
        public BlockTipoEnum Tipo { get; set; }
        public string Color { get => GetEnumDescription(Tipo); }
        public int Tempo { get; set; }
    }
}
