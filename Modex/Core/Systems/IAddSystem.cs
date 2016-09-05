namespace ModexECS
{
    public interface IAddSystem : ISystem
    {
        void OnAdd(IComponent added, Entity entity);
    }
}