using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimuladorRRF.Classes
{
    public class No<T>
    {
        public T Value { get; private set; }
        public No<T> Next { get; set; }

        public No(T value)
        {
            Value = value;
        }

        // Adiciona novo noh como proximo do atual
        public No<T> Add(T value)
        {
            Next = new No<T>(value);
            return Next;
        }

        // Aponta para outro noh e retorna valor do proximo
        public T Remove()
        {
            if (Next == null)
            {
                throw new Exception("Não há nó a ser removido.");
            }
            var VlNext = Next.Value;
            Next = Next.Next;
            return VlNext;
        }
    }
}
