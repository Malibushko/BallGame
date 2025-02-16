namespace Game.Common
{
    public interface IConfigsService
    {
        public bool Load<T>(string path, out T value);
    }
}