using System.Collections.Generic;
using UniRx;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Lean.Pool;
using Constants;

namespace Score
{
    public class ScoreView : MonoBehaviour, IScoreView
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private GameObject _resultPanel;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _endButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private GameObject _rankingRow;
        [SerializeField] private Transform _rankingRowParent;

        private void Awake()
        {
            _resultPanel.SetActive(false);
        }

        public void SetScore(int score)
        {
            _scoreText.text = "Score : " + score;
        }

        /// <summary>
        /// リザルトパネルを表示する
        /// </summary>
        /// <param name="scoreList"></param>
        public void OpenResultPanel(List<int> scoreList)
        {
            _closeButton.gameObject.SetActive(false);
            _resultPanel.SetActive(true);

            // ボタンの購読を開始
            _continueButton.OnButtonObservable()
                .Subscribe(_ => SceneControllerHelper.LoadScene("MainScene"))
                .AddTo(gameObject);

            _endButton.OnButtonObservable()
                .Subscribe(_ => SceneControllerHelper.LoadScene("Title"))
                .AddTo(gameObject);

            for (int i = 0; i < scoreList.Count; i++)
            {
                GameObject row = LeanPool.Spawn(_rankingRow);
                row.transform.parent = _rankingRowParent;
                row.GetComponent<ScoreRowView>().SetScore(i + 1, scoreList[i]);
            }
        }

        public void OpenRunkingPanel(List<int> scoreList)
        {
            _resultPanel.SetActive(true);

            _continueButton.gameObject.SetActive(false);
            _endButton.gameObject.SetActive(false);
            _closeButton.gameObject.SetActive(true);

            _closeButton.OnButtonObservable()
                .Subscribe(_ => GameStore.Instance.SystemStates.RemoveGameState(CGameState.ViewRunking))
                .AddTo(gameObject);

            for (int i = 0; i < scoreList.Count; i++)
            {
                GameObject row = LeanPool.Spawn(_rankingRow);
                row.transform.parent = _rankingRowParent;
                row.GetComponent<ScoreRowView>().SetScore(i + 1, scoreList[i]);
            }
        }

        public void ClosePanel()
        {
            _resultPanel.SetActive(false);
        }
    }
}
