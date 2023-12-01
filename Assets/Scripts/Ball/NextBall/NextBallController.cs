using UnityEngine;
using Ball_Drop;

namespace Ball_Next
{
    public class NextBallController : MonoBehaviour
    {
        private DropController _dropController;
        [SerializeField] private BallList _ballList;
        private int _ballCount;

        private void Awake()
        {
            _dropController = GameObject.FindGameObjectWithTag(TagName.Player).GetComponent<DropController>();
            _ballCount = _ballList.NextBallList.Count;
        }

        private void Start()
        {
            NextBall();
        }

        private void NextBall()
        {
            GameObject nextBall = _ballList.NextBallList[Random.Range(0, _ballCount)];
            _dropController.SetNextBall(nextBall);
        }
    }
}
