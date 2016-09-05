using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using UnityEngine;

namespace ModexECS
{
    public delegate void OnComponentRemoved(IComponent componentRemoved, Entity entity);

    public partial class Modex
    {
        private void AttachRemoverMethodsToNewEntity(Entity newEntity)
        {
            foreach (IRemoveSystem system in systems.FindAll(x => x is IRemoveSystem))
            {
                newEntity.OnComponentRemoved += system.OnRemove;
            }
        }
    }

    public partial class Entity
    {
        public OnComponentRemoved OnComponentRemoved;

        public IComponent RemoveComponent<T>()
        {
            if (!components.Any(x => x is T))
                throw new ModexException("Entity does not contain component of type " + typeof(T));
            var removed = components.First(x => x is T);
            components.Remove(removed);
            if(OnComponentRemoved != null)
                OnComponentRemoved.Invoke(removed, this);
            return removed;
        }

        public IComponent RemoveComponent<T>(T component) where T : IComponent
        {
            if (!components.Contains(component))
                throw new ModexException("Entity does not contain referenced component, cannot be removed");
            return RemoveComponent<T>();
        }
    }
}
