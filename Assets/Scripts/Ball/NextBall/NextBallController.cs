using UnityEngine;
using Ball_Drop;
using UniRx;
using System.Linq;
using Constants;
using Lean.Pool;
using System;

namespace Ball_Next
{
    public class NextBallController : MonoBehaviour
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
    }
}
