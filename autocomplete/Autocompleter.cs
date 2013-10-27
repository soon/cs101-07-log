using System;
using System.Collections.Generic;
using System.Linq;

namespace autocomplete
{
    // Внимание!
    // Есть одна распространенная ловушка при сравнении строк: строки можно сравнивать по-разному:
    // с учетом регистра, без учета, зависеть от кодировки и т.п.
    // В файле словаря все слова отсортированы мтодом StringComparison.OrdinalIgnoreCase.
    // Во всех функциях сравнения строк в C# можно передать способ сравнения.
    class Autocompleter
    {
        private readonly string[] items;
        private _BinarySearcher<string> _searcher;

        public Autocompleter(string[] loadedItems)
        {
            items = loadedItems;
            _searcher = new _BinarySearcher<string>(items, new _MyStringComparer());
        }

        // Найти произвольный элемент словаря, начинающийся с prefix.
        // Ускорьте эту фунцию так, чтобы она работала за O(log(n))

        public string FindByPrefixBF(string prefix)
        {
            foreach(var item in items)
            {
                if(item.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    return item;
            }
            return null;
        }


        public string FindByPrefix(string prefix)
        {
            var i = _searcher.IndexOf(prefix);
            if(i == -1)
            {
                return null;
            }
            else
            {
                return items[i];
            }
        }

        // Найти первые (в порядке следования в файле) count (или меньше, если их меньше count) элементов словаря, 
        // начинающийся с prefix.
        // Эта функция должна работать за O(log(n) + count)

        public string[] FindByPrefixBF(string prefix, int count)
        {
            int b = 0;
            while
            (
                b < items.Length && 
                !items[b].StartsWith(prefix, StringComparison.OrdinalIgnoreCase)
            )
            {
                ++b;
            }

            var e = b;
            while
            (
                e < items.Length && 
                items[e].StartsWith(prefix, StringComparison.OrdinalIgnoreCase) &&
                e - b <= count
            )
            {
                ++e;
            }

            var new_arr = new string[Math.Min(e - b, count)];
            for(int i = 0; i < new_arr.Length; ++i)
            {
                new_arr[i] = items[i + b];
            }

            return new_arr;
        }

        public string[] FindByPrefix(string prefix, int count)
        {
            var b = _searcher.IndexOfFirst(prefix);
            
            if(b == -1)
            {
                return new string[0];
            }

            var e = _searcher.IndexOfPastTheLast(prefix);

            var res = new string[Math.Min(e - b, count)];
            for(int i = 0; i < res.Length; ++i)
            {
                res[i] = items[i + b];
            }

            return res;
        }

        // Найти количество слов словаря, начинающихся с prefix
        // Эта функция должна работать за O(log(n))

        public int FindCountBF(string prefix)
        {
            int b = 0;
            while
            (
                b < items.Length &&
                !items[b].StartsWith(prefix, StringComparison.OrdinalIgnoreCase)
            )
            {
                ++b;
            }

            var e = b;
            while
            (
                e < items.Length &&
                items[e].StartsWith(prefix, StringComparison.OrdinalIgnoreCase)
            )
            {
                ++e;
            }

            return e - b;
        }

        public int FindCount(string prefix)
        {
            if(_searcher.IndexAndBoundsOf(prefix) == -1)
            {
                return 0;
            }
            else
            {
                return _searcher.End - _searcher.Begin;
            }
        }


        /// <summary>
        /// A class for comparing strings
        /// </summary>
        private class _MyStringComparer: IComparer<string>
        {
            /// <summary>Compares two strings, ignoring case</summary>
            /// <returns>Returns 0 if x is started with y
            /// Otherwise behaves like default Compare method</returns>
            public int Compare(string x, string y)
            {
                // TODO
                // Implement this with one loop
                if(x.StartsWith(y, StringComparison.OrdinalIgnoreCase))
                {
                    return 0;
                }
                else
                {
                    return StringComparer.OrdinalIgnoreCase.Compare(x, y);
                }
            }
        }

        /// <summary>
        /// A class for binary searching in an array
        /// </summary>
        private class _BinarySearcher<T>
        {
            private int _begin = -1;
            private int _end = -1;

            private T[] _array;
            

            public _BinarySearcher(T[] array, IComparer<T> comp = null)
            {
                _array = array;
                Comparer = comp ?? Comparer<T>.Default;
            }


            public int Begin
            {
                get
                {
                    return _begin;
                }
            }

            public int End
            {
                get
                {
                    return _end;
                }
            }

            public IComparer<T> Comparer
            {
                get;
                set;
            }


            /// <summary>
            /// Search for x in a whole array
            /// </summary>
            public int IndexOf(T x)
            {
                return IndexOf(x, 0, _array.Length);
            }

            /// <returns>Returns index of x in range [begin, end)
            /// If element does not exist, returns -1</returns>
            public int IndexOf(T x, int begin, int end)
            {
                _begin = Math.Max(begin, 0);
                _end = Math.Min(end, _array.Length);

                while(_begin < _end)
                {
                    var curr = (_begin + _end) / 2;
                    var res = Comparer.Compare(_array[curr], x);

                    if(res < 0)
                    {
                        _begin = curr + 1;
                    }
                    else if(res == 0)
                    {
                        return curr;
                    }
                    else
                    {
                        _end = curr;
                    }
                }

                return -1;
            }

            /// <summary>
            /// Search for first occurense of x in the whole array
            /// </summary>
            public int IndexOfFirst(T x)
            {
                return IndexOfFirst(x, 0, _array.Length);
            }

            /// <returns>Returns index of first occurense of x in the array
            /// If element does not exist, returns -1</returns>
            public int IndexOfFirst(T x, int begin, int end)
            {
                var i = IndexOf(x, begin, end);
                if(i == -1)
                {
                    return -1;
                }

                begin = _begin;
                end = i;

                while(begin < end)
                {
                    var curr = (begin + end) / 2;
                    var res = Comparer.Compare(_array[curr], x);

                    if(res == 0)
                    {
                        end = curr;
                    }
                    else
                    {
                        begin = curr + 1;
                    }
                }

                return begin;
            }

            /// <returns></returns>
            public int IndexOfPastTheLast(T x)
            {
                return IndexOfPastTheLast(x, 0, _array.Length);
            }

            /// <returns>Returns index past the last occurense of x in the array
            /// If element does not exist, returns -1</returns>
            public int IndexOfPastTheLast(T x, int begin, int end)
            {
                var i = IndexOf(x, begin, end);
                if(i == -1)
                {
                    return -1;
                }

                begin = i;
                end = _end;

                while(begin < end)
                {
                    var curr = (begin + end) / 2;
                    var res = Comparer.Compare(_array[curr], x);

                    if(res == 0)
                    {
                        begin = curr + 1;
                    }
                    else
                    {
                        end = curr;
                    }
                }

                return begin;
            }

            /// <summary>
            /// Fucking comments...
            /// </summary>
            /// <param name="x"></param>
            /// <returns></returns>
            public int IndexAndBoundsOf(T x)
            {
                return IndexAndBoundsOf(x, 0, _array.Length);
            }

            /// <summary>Sets left and right bounds, such that all elements in range
            /// [begin, end) are equal to x. 
            /// If element does not exist, Begin == End == -1</summary>
            /// <returns>Index of given element
            /// If element does not exist, returns -1</returns>
            public int IndexAndBoundsOf(T x, int begin, int end)
            {
                var i = IndexOf(x, begin, end);
                if(i == -1)
                {
                    begin = -1;
                    end = -1;
                    return -1;
                }

                _begin = IndexOfFirst(x, _begin, _end);
                _end = IndexOfPastTheLast(x, _begin, _end);

                return i;
            }
        }
    }
}
