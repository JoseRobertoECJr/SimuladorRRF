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
        private readonly No<T> _first;
        private No<T> _last;

        public Fila()
        {
            _first = new No<T>(default(T));
            _last = _first;
        }

        // Adiciona a fila
        public void Add(T value)
        {
            _last = _last.Add(value);

            if(_first.Next == null)
                _first.Next = _last;

            Count++;
        }

        // Remove da fila e retorna o elemento
        public T Remove()
        {
            Count--;
            return _first.Remove();
        }

    }
}
