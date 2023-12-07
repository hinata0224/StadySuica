using Constants;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class SuspendView : MonoBehaviour
{
    [SerializeField] private GameObject _suspendPanel;
    [SerializeField] private Button _suspendButton;
    [SerializeField] private Button _restertButton;

    private CompositeDisposable _disposables = new CompositeDisposable();

    public void Initilaize()
    {
        _suspendPanel.SetActive(false);

        _suspendButton.OnButtonObservable()
            .Subscribe(_ => GameStore.Instance.SystemStates.AddGameState(CGameState.TimeStop))
            .AddTo(gameObject);
    }

    public void DisplayTimeStopPanel(bool isTimeRunning)
    {
        _suspendPanel.SetActive(isTimeRunning);
        if (isTimeRunning)
        {
            _restertButton.OnButtonObservable()
                .Subscribe(_ => GameStore.Instance.SystemStates.RemoveGameState(CGameState.TimeStop))
                .AddTo(_disposables);
        }
        else
        {
            _disposables.Clear();
        }
    }
}
