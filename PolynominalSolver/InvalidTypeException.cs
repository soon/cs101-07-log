using System;
using System.Runtime.Serialization;

namespace Soon
{
    [Serializable]
    public class InvalidTypeException : Exception
    {
        public InvalidTypeException() :
            base()
        {

        }

        public InvalidTypeException(string s) :
            base(s)
        {

        }

        public InvalidTypeException(SerializationInfo i, StreamingContext c) :
            base(i, c)
        {

        }

        public InvalidTypeException(string s, Exception e) :
            base(s, e)
        {

        }
    }
}