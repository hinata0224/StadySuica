using System.Collections.Generic;
using System.Linq;
using UniRx;
using Constants;

public class SystemStates : ISystemState
{
    public SystemStates(
        List<CGameState> cGameStates
    )
    {
        Initialize(cGameStates);
    }

    private void Initialize(
        List<CGameState> cGameStates
    )
    {
        _rpCGameStates = new ReactiveCollection<CGameState>(cGameStates);
    }

    /// <summary> ゲームステート (privateのRP) </summary>
    private ReactiveCollection<CGameState> _rpCGameStates;
    /// <summary> ゲームステート (Subscribe用) </summary>
    public IReadOnlyReactiveCollection<CGameState> RPCGameStates => _rpCGameStates;

    /// <summary>
    /// ゲームステート追加用セッター
    /// </summary>
    /// <param name="value">追加するCGameState</param>
    public void AddGameState(CGameState value)
    {
        if (!RPCGameStates.Contains(value)) _rpCGameStates.Add(value);
    }

    /// <summary>
    /// ゲームステート削除用セッター
    /// </summary>
    /// <param name="value">削除するCGameState</param>
    public void RemoveGameState(CGameState value)
    {
        if (RPCGameStates.Contains(value)) _rpCGameStates.Remove(value);
    }
}
