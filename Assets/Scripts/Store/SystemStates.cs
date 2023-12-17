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
        _rpIsTimeRunning = new ReactiveProperty<bool>();
        UpdateIstimeRunning();
    }

    /// <summary> ゲームステート (privateのRP) </summary>
    private ReactiveCollection<CGameState> _rpCGameStates;
    /// <summary> ゲームステート (Subscribe用) </summary>
    public IReadOnlyReactiveCollection<CGameState> RPCGameStates => _rpCGameStates;
    /// <summary> ゲームステート (読み取り専用のプロパティ) </summary>
    public List<CGameState> CGameStates => _rpCGameStates.ToList();

    /// <summary>
    /// ゲームステート追加用セッター
    /// </summary>
    /// <param name="value">追加するCGameState</param>
    public void AddGameState(CGameState value)
    {
        if (!RPCGameStates.Contains(value)) _rpCGameStates.Add(value);
        UpdateIstimeRunning();
    }

    /// <summary>
    /// ゲームステート削除用セッター
    /// </summary>
    /// <param name="value">削除するCGameState</param>
    public void RemoveGameState(CGameState value)
    {
        if (RPCGameStates.Contains(value)) _rpCGameStates.Remove(value);
        UpdateIstimeRunning();
    }

    /// <summary> 時間が進行中かのフラグ (privateのRP) </summary>
    private ReactiveProperty<bool> _rpIsTimeRunning;
    /// <summary> 時間が進行中かのフラグ (Subscribe用) </summary>
    public IReadOnlyReactiveProperty<bool> RPIsTimeRunning => _rpIsTimeRunning;

    /// <summary> 時間が進行中かのフラグのセッター </summary>
    /// NOTE: CGameStateによって状態が決定するためprivateにしている
    /// <param name="value">値</param>
    private void SetIsTimeRunning(bool value)
    {
        _rpIsTimeRunning.Value = value;
    }

    /// <summary>
    /// 時間が進行中かのフラグを更新する
    /// </summary>
    private void UpdateIstimeRunning()
    {
        bool isTimeRunning = !RPCGameStates.Any(a => timeStopGameStates.Contains(a));
        SetIsTimeRunning(isTimeRunning);
    }

    /// <summary>
    /// 時間が停止するゲームステート一覧
    /// </summary>
    /// <value></value>
    private readonly List<CGameState> timeStopGameStates = new List<CGameState>{
        CGameState.TimeStop,
        CGameState.GameOver,
    };
}
