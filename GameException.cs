using System;
using System.Collections.Generic;
using System.Text;

namespace ConsolQuest
{
    class GameException : Exception
    {
        public GameException()
        {
        }

        public GameException(string message) : base(message)
        {
        }
    }
}
