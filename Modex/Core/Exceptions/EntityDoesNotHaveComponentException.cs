using UnityEngine;
using System.Collections;
using ModexECS;

namespace ModexECS
{
    public class EntityDoesNotHaveComponentException : ModexException
    {
        public EntityDoesNotHaveComponentException(string message) : base(message)
        {            
        }
    }
}

