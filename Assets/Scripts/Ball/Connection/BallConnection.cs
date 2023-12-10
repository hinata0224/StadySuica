using UnityEngine;
using Constants;
using Ball_Next;
using Lean.Pool;
using UniRx;
using Cysharp.Threading.Tasks;
using System;

namespace BAll_Connection
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class BallConnection : MonoBehaviour
    {
        [SerializeField] private float _ballUpLimit = 3.5f;
        [SerializeField] private CBallType _ballType;
        public CBallType BallType => _ballType;
        [HideInInspector] public bool IsDrop = false;
        private bool _isConnect = false;
        public bool IsConnect => _isConnect;
        private bool _isGameOver = false;
        private IBallController _ballController;
        private Rigidbody2D _rb;
        private CircleCollider2D _collider;

        private void Awake()
        {
            _ballController = GameObject.FindGameObjectWithTag(TagName.BallController).GetComponent<BallController>();
            _rb = GetComponent<Rigidbody2D>();
            _collider = GetComponent<CircleCollider2D>();
        }

        private void Start()
        {
            GameStore.Instance.SystemStates.RPIsTimeRunning
                .Where(x => IsDrop)
                .Subscribe(x => SetGravity(x))
                .AddTo(gameObject);
        }
        private void Update()
        {
            if (transform.position.y >= _ballUpLimit && _isGameOver)
            {
                if (!IsDrop)
                {
                    return;
                }
                GameStore.Instance.SystemStates.AddGameState(CGameState.GameOver);
                _isGameOver = false;
            }
        }

        private void OnDisable()
        {
            _isConnect = false;
            IsDrop = false;
            _isGameOver = false;
        }

        private void OnEnable()
        {
            _collider.isTrigger = true;
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
                connectBall.GetComponent<BallConnection>().SetGameOverFlag();
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

        /// <summary>
        /// 停止中は動かないようにする
        /// </summary>
        /// <param name="isTimeRunning"></param>
        public void SetGravity(bool isTimeRunning)
        {
            if (!isTimeRunning)
            {
                _rb.gravityScale = 0;
                _rb.velocity = new Vector2(0, 0);
            }
            else
            {
                _rb.gravityScale = 1;
            }
        }

        /// <summary>
        /// 一秒後にボールのゲームオーバー判定を有効にする
        /// </summary>
        public void SetGameOverFlag()
        {
            _collider.isTrigger = false;
            Observable.Timer(TimeSpan.FromSeconds(0.5f))
                .First()
                .Subscribe(_ => _isGameOver = true)
                .AddTo(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag(TagName.Ball) &&
                other.collider.GetComponent<BallConnection>().BallType == _ballType)
            {
                if (!_isConnect && !other.collider.GetComponent<BallConnection>().IsConnect)
                {
                    _isConnect = true;
                    if (IsSpwanPosition(other.gameObject.transform))
                    {
                        GameStore.Instance.GameData.AddScoreType(_ballType);
                        ConnectBall();
                    }
                    _rb.gravityScale = 0;
                    IsDrop = false;
                    LeanPool.Despawn(gameObject);
                }
            }
        }
    }
}
