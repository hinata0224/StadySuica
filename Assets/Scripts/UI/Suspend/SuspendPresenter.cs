using Constants;
using UniRx;
using UnityEngine;

// TODO: リネーム
public class SuspendPresenter : MonoBehaviour
{
    private SuspendView _view;

    private void Awake()
    {
        _view = GameObject.FindGameObjectWithTag(TagName.SuspendView).GetComponent<SuspendView>();
        _view.Initilaize();
    }

    private void Start()
    {
        GameStore.Instance.SystemStates.RPIsTimeRunning
            .Where(_ => !GameStore.Instance.SystemStates.CGameStates.Contains(CGameState.GameOver))
            .Subscribe(isTimeRunning => _view.DisplayTimeStopPanel(!isTimeRunning))
            .AddTo(gameObject);
    }
}
