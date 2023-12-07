public interface IGameStore
{
    public ISystemState SystemStates { get; }
    public IGameData GameData { get; }
}
