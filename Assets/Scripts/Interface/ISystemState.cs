using Constants;
using UniRx;

public interface ISystemState
{
    public IReadOnlyReactiveCollection<CGameState> RPCGameStates { get; }
    public IReadOnlyReactiveProperty<bool> RPIsTimeRunning { get; }
    /// <summary>
    /// ゲームステート追加用セッター
    /// </summary>
    /// <param name="value">追加するCGameState</param>
    public void AddGameState(CGameState value);
    /// <summary>
    /// ゲームステート削除用セッター
    /// </summary>
    /// <param name="value">削除するCGameState</param>
    public void RemoveGameState(CGameState value);
}
