using Ball_Data;
using Constants;
using UnityEngine;

namespace Score
{
    public class ScoreModel
    {
        public ScoreModel(BallList ballList)
        {
            Initilaize(_ballList);
        }

        private BallList _ballList;
        private int _score;

        private void Initilaize(BallList ballList)
        {
            _ballList = ballList;
            _score = 0;
        }

        /// <summary>
        /// スコアの加算
        /// </summary>
        /// <param name="type"></param>
        public void AddScore(CBallType type)
        {
            foreach (BallData data in _ballList.NextBallList)
            {
                if (data.BallType == type)
                {
                    _score += data.Score;
                    break;
                }
            }
        }

        /// <summary>
        /// スコアのセーブ
        /// </summary>
        public void SaveScore()
        {
            GameStore.Instance.GameData.AddScore(_score);
            GameStore.Instance.SystemStates.AddGameState(CGameState.ScoreSaveEnd);
        }
    }
}
