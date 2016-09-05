namespace ModexECS
{
    public interface IModifySystem : ISystem
    {
        void OnModify(IComponent modified, Entity entity);
    }
}
