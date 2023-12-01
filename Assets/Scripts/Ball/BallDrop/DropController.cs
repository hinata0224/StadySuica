using UnityEngine;
using InputProvider;
using UniRx;
using System;

namespace Ball_Drop
{
    public class DropController : MonoBehaviour, IDorpController
    {
        [SerializeField] private float _maximumCameraMovableLimit = 5f;
        [SerializeField] private float _speed = 0.02f;
        [SerializeField] private Transform _ballPosition;

        private GameObject _ballObject;
        private CompositeDisposable disposables = new CompositeDisposable();

        private IInputProvider _inputProvider;
        private ISystemState _systemState;

        private void Awake()
        {
            _inputProvider = UnityInputProvider.Instance;
            _systemState = GameStore.Instance.SystemStates;
        }

        private void Start()
        {
            _inputProvider.OnSubmitObservable
                .Where(_ => _ballObject != null)
                .Subscribe(_ => DropBall())
                .AddTo(gameObject);
        }

        private void Update()
        {
            HorizontalMove();
        }

        #region Move
        /// <summary>
        /// 左右の移動
        /// </summary>
        private void HorizontalMove()
        {
            float inputX = Input.GetAxisRaw("Horizontal");

            if (inputX == 0)
            {
                return;
            }

            Vector2 positon = transform.position;
            if (Mathf.Sign(inputX) == 1)
            {
                positon.x += _speed;
            }
            else
            {
                positon.x -= _speed;
            }

            transform.position = MovableRangeControlOfCamera(positon.x);
        }

        /// <summary>
        /// 移動可能範囲制御
        /// </summary>
        /// <param name="inputX">移動後の位置</param>
        /// <returns>移動後の位置</returns>
        private Vector2 MovableRangeControlOfCamera(float inputX)
        {
            // 制限範囲外だったら現在の座標から制限範囲内までの座標を返す
            // X座標
            if (inputX >= 0 && inputX >= _maximumCameraMovableLimit)
            {
                inputX = _maximumCameraMovableLimit;
            }
            else if (inputX < 0 && inputX < -_maximumCameraMovableLimit)
            {
                inputX = -_maximumCameraMovableLimit;
            }

            return new Vector2(inputX, transform.position.y);
        }
        #endregion

        #region  Drop

        /// <summary>
        /// 弾を落とす
        /// </summary>
        private void DropBall()
        {
            _ballObject.transform.parent = null;
            _ballObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            _ballObject = null;

            // 1 秒後にステートを更新
            // 秒数は適当
            Observable.Timer(TimeSpan.FromSeconds(1))
                .Subscribe(_ => _systemState.AddGameState(Constants.CGameState.NextBall))
                .AddTo(disposables);
        }

        #endregion

        #region  Next

        /// <summary>
        /// 次のオブジェクトをセット
        /// </summary>
        /// <param name="ball">次のオブジェクト</param>
        public void SetNextBall(GameObject ball)
        {
            _ballObject = ball;
            _ballObject.transform.parent = _ballPosition;
            _ballObject.transform.localPosition = new Vector3(0, 0, 0);
            disposables.Clear();
        }

        #endregion
    }
}
