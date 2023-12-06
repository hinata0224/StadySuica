using TMPro;
using UnityEngine;

namespace Score
{
    public class ScoreRowView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _numberText;
        [SerializeField] private TextMeshProUGUI _scoreText;

        /// <summary>
        /// スコアをセット
        /// </summary>
        /// <param name="num">順位</param>
        /// <param name="score">スコア</param>
        public void SetScore(int num, int score)
        {
            _numberText.text = num.ToString();
            _scoreText.text = score.ToString();
        }
    }
}
