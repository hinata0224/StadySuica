using Ball_Data;
using Constants;
using UniRx;
using UnityEngine;

namespace Score
{
    public class ScorePresenter : MonoBehaviour
    {
        [SerializeField] private BallList _ballData;

        private IScoreModel _model;
        private ScoreView _scoreView;
        private GameStore _gameStore;

        private void Awake()
        {
            _model = new ScoreModel(_ballData);
            _scoreView = GameObject.FindGameObjectWithTag(TagName.ScoreView).GetComponent<ScoreView>();
            _gameStore = GameStore.Instance;
        }

        private void Start()
        {
            _gameStore.GameData.ConnectBallType
                .Subscribe(type => _model.AddScore(type))
                .AddTo(gameObject);

            _model.RPScore
                .Subscribe(x => _scoreView.SetScore(x))
                .AddTo(gameObject);

            _gameStore.SystemStates.RPCGameStates
                .ObserveAdd()
                .Where(x => x.Value == CGameState.GameOver)
                .Subscribe(_ => EndGame())
                .AddTo(gameObject);
        }

        /// <summary>
        /// ゲームオーバー時の処理
        /// </summary>
        private void EndGame()
        {
            // _gameStore.SystemStates.AddGameState(CGameState.TimeStop);
            _model.SaveScore();
            _scoreView.OpenResultPanel(_gameStore.GameData.GetScoreRanking());
        }
    }
}
