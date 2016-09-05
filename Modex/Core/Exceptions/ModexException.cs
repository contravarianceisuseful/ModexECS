using System;

namespace ModexECS
{
    public class ModexException : Exception
    {
        public ModexException(string message) : base(message) { }
    }
}