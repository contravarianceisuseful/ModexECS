namespace ModexECS
{
    /// <summary>
    /// Template class that implements the IRemoveSystem interface
    /// </summary>
    /// <typeparam name="T">The type of component you want OnRemove to be triggered by</typeparam>
    /// <seealso cref="ModexECS.IRemoveSystem" />
    public abstract class OnRemoveSystem<T> : IRemoveSystem where T : IComponent
    {
        protected Pool pool;

        /// <summary>
        /// The component that was removed casted to the desired type.
        /// </summary>
        /// <value>
        /// The Removeed component.
        /// </value>
        public T RemovedComponent { get; set; }

        public virtual void SetPool(Pool pool)
        {
            this.pool = pool;
        }

        public bool CheckComponent(IComponent component)
        {
            return component is T;
        }

        public void OnRemove(IComponent component, Entity entity)
        {
            if (CheckComponent(component))
            {
                RemovedComponent = (T)component;
                OnRemove(entity);
            }
        }

        public abstract void OnRemove(Entity entity);
    }
}