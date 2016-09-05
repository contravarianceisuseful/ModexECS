namespace ModexECS
{
    public delegate void OnComponentAdded(IComponent componentAdded, Entity entity);

    public partial class Modex
    {
        private void AttachAdderMethodsToNewEntity(Entity newEntity)
        {
            foreach (IAddSystem system in systems.FindAll(x => x is IAddSystem))
            {                
                newEntity.OnComponentAdded += system.OnAdd;               
            }
        }      
    }

    public partial class Entity
    {
        public OnComponentAdded OnComponentAdded;

        public Entity AddComponent(IComponent newComponent)
        {
            if (GetComponents().Contains(newComponent))
                throw new ModexException("Entity already contains a component of type: " + newComponent.GetType());
            components.Add(newComponent);
            if(OnComponentAdded != null)
                OnComponentAdded.Invoke(newComponent, this);
            return this;
        }
    }


}
