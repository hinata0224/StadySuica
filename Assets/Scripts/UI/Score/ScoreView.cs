using System.Collections.Generic;
using UniRx;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Lean.Pool;

namespace Score
{
    public class ScoreView : MonoBehaviour, IScoreVire
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private GameObject _resultPanel;
        [SerializeField] private TextMeshProUGUI _resultScoreText;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _endButton;
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

        public void OpenResultPanel(List<int> scoreList, int score)
        {
            _resultPanel.SetActive(true);
            _resultScoreText.text += score.ToString();

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
    }
}
