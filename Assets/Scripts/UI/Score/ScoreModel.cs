using Ball_Data;
using Constants;
using UniRx;

namespace Score
{
    public class ScoreModel : IScoreModel
    {
        public ScoreModel(BallList ballList)
        {
            Initilaize(ballList);
        }

        private BallList _ballList;
        private ReactiveProperty<int> _rpScore = new ReactiveProperty<int>();
        public IReadOnlyReactiveProperty<int> RPScore => _rpScore;

        private void Initilaize(BallList ballList)
        {
            _ballList = ballList;
            _rpScore.Value = 0;
        }

        /// <summary>
        /// スコアの加算
        /// </summary>
        /// <param name="type"></param>
        public void AddScore(CBallType type)
        {
            foreach (BallData data in _ballList.BallLists)
            {
                if (data.BallType == type)
                {
                    _rpScore.Value += data.Score;
                    break;
                }
            }
        }

        /// <summary>
        /// スコアのセーブ
        /// </summary>
        public void SaveScore()
        {
            GameStore.Instance.GameData.SaveScore(_rpScore.Value);
        }
    }
}
