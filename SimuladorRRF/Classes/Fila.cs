using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimuladorRRF.Classes
{
    public class Fila<T>
    {
        // Contador de itens da fila
        public int Count { get; private set; }

        // Primeiro e ultimo da fila
        private readonly No<T> First;
        private No<T> Last;

        public Fila()
        {
            First = new No<T>(default(T));
            Last = First;
        }

        // Adiciona a fila
        public void Add(T value)
        {
            Last = Last.Add(value);
            Count++;
        }

        // Remove da fila e retorna o elemento
        public T Remove()
        {
            Count--;
            return First.Remove();
        }

    }
}
