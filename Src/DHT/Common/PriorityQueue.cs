// Developed by doiTTeam => devdoiTTeam@gmail.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DHT
{
    public class PriorityQueue<T> : IEnumerable<T>
    {
        private readonly Func<T, T, int> _comparer;

        private readonly T[] _items;

        private int _count;

        public PriorityQueue(int maxItems, Func<T, T, int> comparer)
        {
            _comparer = comparer;

            _items = new T[maxItems];
        }

        public int Count
        {
            get { return _count; }
        }

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return _items.OfType<T>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        private bool Exist(int node)
        {
            return node > 0 && node < _items.Length
                   && node < _count;
        }

        private void Swap(ref T a, ref T b)
        {
            T t = a;
            a = b;
            b = t;
        }

        private int Padre(int node)
        {
            return (node - 1)/2;
        }

        private int Izq(int node)
        {
            return 2*node + 1;
        }

        private int Der(int node)
        {
            return 2*node + 2;
        }

        private void Heapify(int node)
        {
            //el elemento que esta en la raiz se empuja hacia abajo hasta que quepa

            if (Exist(Izq(node)) && _comparer(_items[node], _items[Izq(node)]) > 0) //la raiz es mayor que el hijo izq
            {
                Swap(ref _items[node], ref _items[Izq(node)]);
                Heapify(Izq(node));
                return;
            }
            if (Exist(Der(node)) && _comparer(_items[node], _items[Der(node)]) > 0)
                //la raiz es mayor que el hijo derecho
            {
                Swap(ref _items[node], ref _items[Der(node)]);
                Heapify(Der(node));
                return;
            }
        }

        public void Enqueue(T item)
        {
            if (_count == _items.Length)
                throw new InvalidOperationException("The Queue is full.");

            _items[_count] = item;
            int i = _count;
            while (i > 0)
            {
                if (_comparer(_items[i], _items[Padre(i)]) < 0) //es menor que su padre
                    Swap(ref _items[i], ref _items[Padre(i)]);
                else break;
                i = Padre(i);
            }

            _count++;
        }

        public T Dequeue()
        {
            if (_count == 0)
                throw new InvalidOperationException("The Queue is empty.");

            T res = _items[0];
            _items[0] = _items[_count - 1];
            _count--;
            Heapify(0);

            return res;
        }

        public T Peek()
        {
            return _items[0];
        }
    }
}