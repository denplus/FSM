using Data;

namespace Services.Interfaces
{
    public interface IPersistentService
    {
        public PersistentData Load();
        public void Save();
    }
}