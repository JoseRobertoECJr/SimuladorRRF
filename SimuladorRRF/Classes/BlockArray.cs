using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SimuladorRRF.Utils;

namespace SimuladorRRF.Classes
{
    public class BlockArray
    {
        public Block[] Value { get; private set; }
        private int _max = 1000;

        public BlockArray()
        {
            Value = new Block[_max];
        }

        public BlockArray(BlockArray blocks)
        {
            Value = new Block[_max];
            for (var i = 0; i < _max; i++)
            {
                if(blocks.Value != null)
                    Value[i] = blocks.Value[i];
            }
        }

        public void Add(Block v)
        {
            for (var i = 0; i < _max; i++)
            {
                if (Value[i] == null)
                {
                    Value[i] = v;
                    break;
                }

            }
        }

        public int Count
        {
            get
            {

                var count = 0;

                for (var i = 0; i < _max; i++)
                {
                    if (Value[i] != null)
                        count++;
                }

                return count;
            }
        }

        public Block Last()
        {

            Block v = new Block();
            for (var i = 0; i < _max; i++)
            {
                if (Value[i] != null && Value[i].Tempo != 0)
                    v = Value[i];
            }

            return v;
        }

        public int TurnAround
        {
            get
            {
                var count = 0;

                for (var i = 0; i < _max; i++)
                {
                    if (Value[i] != null && Value[i].Tempo != 0 && Value[i].Tipo != BlockTipoEnum.NonExec)
                        count++;
                }

                return count;
            }
        }
    }
}
