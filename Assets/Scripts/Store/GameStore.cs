using System.Collections.Generic;
using Constants;

public class GameStore : SingletonMonoBehaviour<GameStore>, IGameStore
{
    private SystemStates _systemStates;
    public SystemStates SystemStates => _systemStates;

    protected override void Awake()
    {
        base.Awake();
        // システムステートの初期値セット
        SetSystemStates(GetInitSystemStates());
    }

    /// <summary> システムステートのセット </summary>
    /// <param name="value">値</param>
    public void SetSystemStates(SystemStates value)
    {
        _systemStates = value;
    }

    /// <summary>
    /// システムステートの初期値を取得する
    /// </summary>
    /// <returns>PlayData</returns>
    public SystemStates GetInitSystemStates()
    {
        List<CGameState> initCGameStates = new List<CGameState>
        {
            //TODO: タイトル作成時に追加
        };

        return new SystemStates(initCGameStates);
    }
}
