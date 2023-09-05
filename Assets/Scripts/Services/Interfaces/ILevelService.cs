namespace Services.Interfaces
{
    public interface ILevelService
    {
        int CurrentLevel { get; }
        void IncreaseLevel();
    }
}