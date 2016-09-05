using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ModexECS
{
    public partial class Entity
    {
        public Entity()
        {
            components = new List<IComponent>();
        }

        private List<IComponent> components;

        public List<IComponent> GetComponents(Predicate<IComponent> condition)
        {
            var returnList = new List<IComponent>(components).Where(x => condition(x)) as List<IComponent>;
            return returnList;
        }

        public List<IComponent> GetComponents()
        {
            return new List<IComponent>(components);
        }

        public T GetComponent<T>() where T : IComponent
        {
            return (T)GetComponents().First(x => x is T);
        }
    }
}

