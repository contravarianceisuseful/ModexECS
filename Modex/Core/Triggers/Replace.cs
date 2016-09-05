namespace ModexECS
{
    public partial class Entity
    {
        public Entity ReplaceComponent(IComponent component)
        {
            RemoveComponent(component);
            return AddComponent(component);
        }
    }
}