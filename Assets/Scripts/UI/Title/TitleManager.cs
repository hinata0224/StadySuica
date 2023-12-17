using Score;
using UnityEngine;
using UniRx;
using Constants;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _runkingButton;

    private IScoreView _scoreView;
    private GameStore _gameStore;

    private void Awake()
    {
        _scoreView = GameObject.FindGameObjectWithTag(TagName.ScoreView).GetComponent<ScoreView>();
        _gameStore = GameStore.Instance;
    }

    void Start()
    {
        _startButton.OnButtonObservable()
            .Subscribe(_ => SceneControllerHelper.LoadScene("MainScene"))
            .AddTo(gameObject);

        _runkingButton.OnButtonObservable()
            .Subscribe(_ => _gameStore.SystemStates.AddGameState(CGameState.ViewRunking))
            .AddTo(gameObject);

        _gameStore.SystemStates.RPCGameStates
            .ObserveAdd()
            .Where(x => x.Value == CGameState.ViewRunking)
            .Subscribe(_ => _scoreView.OpenRunkingPanel(_gameStore.GameData.GetScoreRanking()))
            .AddTo(gameObject);

        _gameStore.SystemStates.RPCGameStates
            .ObserveRemove()
            .Where(x => x.Value == CGameState.ViewRunking)
            .Subscribe(_ => _scoreView.ClosePanel())
            .AddTo(gameObject);
    }
}
