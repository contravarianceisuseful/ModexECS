namespace ModexECS
{
    /// <summary>
    /// Template class that implements the IAddSystem interface
    /// </summary>
    /// <typeparam name="T">The type of component you want OnAdd to be triggered by</typeparam>
    /// <seealso cref="ModexECS.IAddSystem" />
    public abstract class OnAddSystem<T> : IAddSystem where T : IComponent
    {
        protected Pool pool;

        /// <summary>
        /// The component that was added after a cast to the desired type.
        /// </summary>
        /// <value>
        /// The added component.
        /// </value>
        public T AddedComponent { get; set; }

        public virtual void SetPool(Pool pool)
        {
            this.pool = pool;
        }

        public bool CheckComponent(IComponent component)
        {
            return component is T;
        }

        public void OnAdd(IComponent component, Entity entity)
        {
            if (CheckComponent(component))
            {
                AddedComponent = (T) component;
                OnAdd(entity);
            }
        }

        public abstract void OnAdd(Entity entity);
    }
}