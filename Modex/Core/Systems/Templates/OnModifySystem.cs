namespace ModexECS
{
    /// <summary>
    /// Template class that implements the IModifySystem interface
    /// </summary>
    /// <typeparam name="T">The type of component you want OnModify to be triggered by</typeparam>
    /// <seealso cref="IModifySystem" />
    public abstract class OnModifySystem<T> : IModifySystem where T : IModifiableComponent
    {
        protected Pool pool;

        /// <summary>
        /// The component that was Modifyd casted to the desired type.
        /// </summary>
        /// <value>
        /// The Modifyed component.
        /// </value>
        public T ModifiedComponent { get; set; }

        public T originalComponent;

        public virtual void SetPool(Pool pool)
        {
            this.pool = pool;
        }

        public bool CheckComponent(IComponent component)
        {
            return component is T;
        }

        public void OnModify(IComponent component, Entity entity)
        {
            if (CheckComponent(component))
            {
                ModifiedComponent = (T)component;
                OnModify(entity);
            }
        }

        public abstract void OnModify(Entity entity);
    }
}