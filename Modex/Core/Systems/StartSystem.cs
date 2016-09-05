using System;
using UnityEngine;
using System.Collections;
using System.Linq;

namespace ModexECS
{
    public partial class Modex
    {
        /// <summary>
        /// puts the system into the ECS.
        /// Does not check for duplicates.
        /// </summary>
        /// <param name="system">The system.</param>
        public Modex StartSystem(ISystem system)
        {
            //Add new system's OnAdd method to each entity;
            var addSystem = system as IAddSystem;
            if (addSystem != null)
            {
                foreach (var entity in Pool)
                {
                    entity.OnComponentAdded += addSystem.OnAdd;
                }
            }
            systems.Add(system);
            Debug.Log("New system started.");
            return this;
        }

        /// <summary>
        /// Creates a new system of the given type 
        /// and starts it. 
        /// Does check for duplicates. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="DuplicateSystemException">Trying to add a system already present</exception>
        public Modex StartSystem<T>() where T : ISystem
        {
            if(systems.Any(x => x is T))
                throw new DuplicateSystemException("Trying to add a system already present");
            var system = Activator.CreateInstance<T>();
            return StartSystem(system);
        }
    }
}

