using UnityEngine;
using Constants;
using Ball_Next;
using Lean.Pool;

namespace BAll_Connection
{
    public class BallConnection : MonoBehaviour
    {
        [SerializeField] private CBallType _ballType;
        [HideInInspector] public CBallType BallType => _ballType;
        private IBallController _ballController;

        private void Awake()
        {
            _ballController = GameObject.FindGameObjectWithTag(TagName.BallController).GetComponent<BallController>();
        }

        /// <summary>
        /// ボールの合成
        /// </summary>
        private void ConnectBall()
        {
            GameObject connectBall = _ballController.FindBall(_ballType);
            if (connectBall != null)
            {
                connectBall.GetComponent<Rigidbody2D>().gravityScale = 1;
                connectBall.transform.localPosition = transform.localPosition;
            }
        }

        /// <summary>
        /// 生成する場所を判別
        /// </summary>
        /// <param name="target">オブジェクト</param>
        /// <returns>bool</returns>
        private bool IsSpwanPosition(Transform target)
        {
            Vector3 originPos = new Vector3(0, -4, 0);
            float thisDistance = (originPos - transform.localPosition).sqrMagnitude;
            float targetDistance = (originPos - target.transform.localPosition).sqrMagnitude;

            if (thisDistance < targetDistance)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag(TagName.Ball) &&
                other.collider.GetComponent<BallConnection>().BallType == _ballType)
            {
                if (IsSpwanPosition(other.gameObject.transform))
                {
                    GameStore.Instance.GameData.AddScoreType(_ballType);
                    ConnectBall();
                }
                GetComponent<Rigidbody2D>().gravityScale = 0;
                LeanPool.Despawn(gameObject);
            }
        }
    }
}
