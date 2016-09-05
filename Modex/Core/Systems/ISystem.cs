using System;
using UnityEngine;


namespace ModexECS
{
    public interface ISystem
    {
        void SetPool(Pool pool);
        bool CheckComponent(IComponent component);
    }
}