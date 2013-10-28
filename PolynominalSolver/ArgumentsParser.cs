using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soon
{
    /// <summary>
    /// A class for parsing arguments
    /// </summary>
    public class ArgumentsParser
    {
        private string[] _args;
        private int _pos;

        public ArgumentsParser(string[] args)
        {
            _args = args;
            _pos = 0;
        }

        /// <returns>Default converter(T.Parse or method from Convert) if exists
        /// Otherwise, returns null</returns>
        public Func<string, T> GetDefaultConverter<T>()
        {
            var converter = typeof(T).GetMethod("Parse",
                                                 new[] { typeof(string) }) ??
                typeof(System.Convert).GetMethod("To" + typeof(T).Name,
                                                 new[] { typeof(string) });
            if(converter == null)
            {
                return null;
            }
            else
            {
                return (s => (T)converter.Invoke(null, new object[] { s }));
            }
        }

        /// <summary>
        /// Converts next element to T using given converter.
        /// If converter is not passes, using 
        /// If there is no elements, position is not moved
        /// </summary>
        /// <returns>A return value indicates whether the conversion succeeded.</returns>
        public bool TryParseNext<T>(out T res, Func<string, T> converter = null)
        {            
            try
            {
                res = ParseNext<T>(converter);
                return true;
            }
            catch
            {
                res = default(T);
                return false;
            }
        }

        /// <summary>
        /// Converts next element to T using given converter
        /// If there is no elements, position is not moved and IndexOutOfRangeException is thrown
        /// </summary>
        /// <returns>Converted element</returns>
        public T ParseNext<T>(Func<string, T> converter = null)
        {
            converter = converter ?? GetDefaultConverter<T>();
            if(converter == null)
            {
                throw new Soon.InvalidTypeException("You have to define a Parse method");
            }

            try
            {
                return converter(_args[_pos++]);
            }
            catch(IndexOutOfRangeException)
            {
                --_pos;
                throw new IndexOutOfRangeException("Nothing to convert more");
            }
        }

        /// <summary>
        /// Parses all possible elements to T using given converter
        /// </summary>
        /// <returns>List with converted elements</returns>
        public List<T> ParseAllPossible<T>(Func<string, T> converter = null)
        {
            var res = new List<T>();

            while(true)
            {
                try
                {
                    res.Add(ParseNext<T>(converter));
                }
                catch
                {
                    break;
                }
            }

            return res;
        }
    }
}
