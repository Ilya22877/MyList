using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MyList
{
    public class MyList<T> : IList<T>
    {
        private const int DefaultCapacity = 4;

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        private T[] Array { get; set; }

        public MyList() : this(DefaultCapacity)
        {
        }

        public MyList(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be more when 0");
            }

            Array = new T[capacity];
        }

        public MyList(IEnumerable<T> collection)
        {
            Array = collection.ToArray();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            if (Count == Array.Length)
            {
                IncreaseArraySize();
            }
            Array[Count] = item;
            Count++;
        }

        public void Clear()
        {
            Array = new T[Array.Length];
            Count = 0;
        }

        public bool Contains(T item)
        {
            for (var i = 0; i < Count; i++)
            {
                if (Array[i].Equals(item))
                {
                    return true;
                }
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            System.Array.Copy(Array, 0, array, arrayIndex, Count);
        }

        public bool Remove(T item)
        {
            var index = IndexOf(item);
            if (index >= 0)
            {
                RemoveAt(index);
                return true;
            }

            return false;
        }

        public int IndexOf(T item)
        {
            return System.Array.IndexOf(Array, item, 0, Count);
        }

        public void Insert(int index, T item)
        {
            if (index > Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (Count == Array.Length) IncreaseArraySize();
            System.Array.Copy(Array, index, Array, index + 1, Count - index);
            Array[index] = item;
            Count++;
        }

        public void RemoveAt(int index)
        {
            if (index > Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            Count--;
            if (index < Count)
            {
                System.Array.Copy(Array, index + 1, Array, index, Count - index);
            }
            Array[Count] = default(T);
        }

        public T this[int index]
        {
            get
            {
                if (index >= Count)
                {
                    throw new ArgumentOutOfRangeException();
                }
                return Array[index];
            }

            set
            {
                if (index >= Count)
                {
                    throw new ArgumentOutOfRangeException();
                }
                Array[index] = value;
            }
        }

        private void IncreaseArraySize()
        {
            var newCapacity = Array.Length == 0 ? 
                DefaultCapacity : 
                Array.Length * 2;
            var newArray = new T[newCapacity];
            Array.CopyTo(newArray, 0);
            Array = newArray;
        }

        public struct Enumerator : IEnumerator<T>
        {
            private readonly MyList<T> _list;
            private int _index;
            private T _current;

            internal Enumerator(MyList<T> list)
            {
                _list = list;
                _index = 0;
                _current = default(T);
            }

            public bool MoveNext()
            {
                if (_index < _list.Count)
                {
                    _current = _list[_index];
                    _index++;
                    return true;
                }

                return false;
            }

            public T Current => _current;

            Object IEnumerator.Current => Current;

            void IEnumerator.Reset()
            {
                _index = 0;
                _current = default(T);
            }

            public void Dispose()
            {
            }
        }
    }
}
