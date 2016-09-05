using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ModexECS;

namespace ModexECS
{
    public partial class Modex 
    {
        #region Fields
        public readonly Pool Pool;
        private List<ISystem> systems;
#endregion

        #region Constructors
        public Modex()
        {
            Pool = new Pool();
            systems = new List<ISystem>();

            systems.ForEach(x => x.SetPool(Pool));
        }
#endregion

        #region Create new entity
        public Entity CreateEntity()
        {
            var entity = new Entity();
            Pool.Add(entity);

            AttachAdderMethodsToNewEntity(entity);
            AttachRemoverMethodsToNewEntity(entity);
            AttachModifierMethodsToNewEntity(entity);

            return entity;
        }
#endregion

        #region Remove Entity
        public Entity RemoveEntity(Entity entity)
        {
            foreach (var component in entity.GetComponents())
            {
                entity.RemoveComponent(component);
            }
            Pool.Remove(entity);
            return entity;
        }
#endregion
    }
}


public class Pool : List<Entity>
{
    
}

