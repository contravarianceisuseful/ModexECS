namespace ModexECS
{
    public interface IRemoveSystem : ISystem
    {
        void OnRemove(IComponent removed, Entity entity);
    }
}