﻿using System;
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

        public Block()
        {

        }

        public Block(Block block)
        {
            Tipo = block.Tipo;
            Tempo = block.Tempo;
        }

        public Block(BlockTipoEnum tipo, int tempo)
        {
            Tipo = tipo;
            Tempo = tempo;
        }
    }
}
