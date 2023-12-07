using UnityEngine;
using Ball_Drop;
using UniRx;
using System.Linq;
using Constants;
using Lean.Pool;
using Ball_Data;

namespace Ball_Next
{
    public class BallController : MonoBehaviour, IBallController
    {
        private int _ballCount;
        [SerializeField] private BallList _ballList;
        private IDorpController _dropController;
        private ISystemState _systemState;

        private void Awake()
        {
            _dropController = GameObject.FindGameObjectWithTag(TagName.Player).GetComponent<DropController>();
            _systemState = GameStore.Instance.SystemStates;
            _ballCount = _ballList.NextBallList.Count;
        }

        private void Start()
        {
            NextBall();
            _systemState.RPCGameStates
                .ObserveAdd()
                .Where(x => x.Value == CGameState.NextBall)
                .Subscribe(_ => NextBall())
                .AddTo(gameObject);
        }

        /// <summary>
        /// 次のオブジェクトを渡す
        /// </summary>
        private void NextBall()
        {
            GameObject nextBall = _ballList.NextBallList[UnityEngine.Random.Range(0, _ballCount)];
            nextBall = LeanPool.Spawn(nextBall);
            _dropController.SetNextBall(nextBall);
            _systemState.RemoveGameState(CGameState.NextBall);
        }

        /// <summary>
        /// ボールに一致したオブジェクトを返す
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public GameObject FindBall(CBallType type)
        {
            GameObject ball;
            switch (type)
            {
                case CBallType.Small:
                    ball = LeanPool.Spawn(_ballList.NextBallList[1]);
                    break;
                case CBallType.Middle:
                    ball = LeanPool.Spawn(_ballList.NextBallList[2]);
                    break;
                case CBallType.Major:
                    ball = null;
                    break;
                default:
                    ball = null;
                    break;
            }
            return ball;
        }
    }
}
