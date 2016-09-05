using UnityEngine;
using System.Collections;
using ModexECS;

namespace ModexECS
{
    public class DuplicateSystemException : ModexException
    {
        public DuplicateSystemException(string message) : base(message)
        {
        }
    }
}

