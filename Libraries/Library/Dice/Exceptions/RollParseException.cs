using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectInspiration.Library.Dice.Exceptions
{
    public class RollParseException : ArgumentException
    {
        public RollParseException()
        {
        }

        public RollParseException(String msg) : base(msg)
        {
        }

        public RollParseException(String msg, Exception inner) : base(msg, inner)
        {
        }
    }
}
