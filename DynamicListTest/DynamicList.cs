using System;
using System.Collections;
using System.Collections.Generic;

namespace DynamicListTest
{
    public class DynamicList<T> : IEnumerable<T>
    {
        private T[] _items;
        private static readonly T[] _emptyArray = new T[0];
        private const int DefaultSize = 4;
        private const int MaxSize = 2146435071;
        
        private struct Enumerator : IEnumerator<T>
        {
            private readonly DynamicList<T> _dynamicList;
            private int _index;
            private T _current;
            T IEnumerator<T>.Current => _current;

            public Enumerator(DynamicList<T> dynamicList) : this()
            {
                _dynamicList = dynamicList;
                _index = 0;
                _current = default;
            }

            public bool MoveNext()
            {
                var list = _dynamicList;
                if ((uint) _index < (uint) list.Count)
                {
                    _current = list._items[_index];
                    _index++;
                    return true;
                }

                _index = _dynamicList.Count + 1;
                _current = default;
                return false;
            }

            public void Reset()
            {
                _index = 0;
                _current = default;
            }

            public object? Current { get; }

            public void Dispose()
            {
            }
        }

        public DynamicList()
        {
            _items = _emptyArray;
        }

        public DynamicList(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException();

            _items = capacity == 0 ? _emptyArray : new T[capacity];
        }

        public int Capacity
        {
            get => _items.Length;
            set
            {
                if (value == _items.Length) return;
                if (value > 0)
                {
                    var newItems = new T[value];
                    if (Count > 0)
                    {
                        Array.Copy(_items, 0, newItems, 0, Count);
                    }

                    _items = newItems;
                }
                else
                {
                    _items = _emptyArray;
                }
            }
        }

        public int Count { get; private set; }

        public T this[int index]
        {
            get
            {
                if ((uint) index >= (uint) Count)
                {
                    throw new ArgumentOutOfRangeException();
                }

                return _items[index];
            }

            set
            {
                if ((uint) index >= (uint) Count)
                {
                    throw new ArgumentOutOfRangeException();
                }

                _items[index] = value;
            }
        }

        public void Add(T item)
        {
            var array = _items;
            var size = Count;
            if ((uint) size < (uint) array.Length)
            {
                Count = size + 1;
                array[size] = item;
            }
            else
            {
                AddWithResize(item);
            }
        }

        private void AddWithResize(T item)
        {
            var size = Count;
            PrepareResize(size + 1);
            Count = size + 1;
            _items[size] = item;
        }

        private void PrepareResize(int min)
        {
            if (_items.Length >= min) return;
            var newCapacity = _items.Length == 0 ? DefaultSize : _items.Length * 2;
            if ((uint) newCapacity > MaxSize) newCapacity = MaxSize;
            if (newCapacity < min) newCapacity = min;
            Capacity = newCapacity;
        }

        public bool Remove(T item)
        {
            var index = IndexOf(item);
            if (index < 0) return false;
            RemoveAt(index);
            return true;
        }

        public int IndexOf(T item) => Array.IndexOf(_items, item, 0, Count);

        public void RemoveAt(int index)
        {
            if ((uint) index >= (uint) Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            Count--;
            if (index < Count)
            {
                Array.Copy(_items, index + 1, _items, index, Count - index);
            }
        }

        public void Clear()
        {
            var size = Count;
            Count = 0;
            if (size > 0)
            { 
                Array.Clear(_items, 0, size); // Clear the elements so that the gc can reclaim the references.
            }
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}