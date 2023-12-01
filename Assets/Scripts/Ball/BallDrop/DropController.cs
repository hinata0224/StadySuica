using UnityEngine;
using InputProvider;
using UniRx;

namespace Ball_Drop
{
    public class DropController : MonoBehaviour
    {
        [SerializeField] private float _maximumCameraMovableLimit = 5f;
        [SerializeField] private float _speed = 0.02f;
        [SerializeField] private Transform _ballPosition;

        private GameObject _ballObject;

        private IInputProvider inputProvider;

        private void Awake()
        {
            inputProvider = UnityInputProvider.Instance;
        }

        private void Start()
        {
            inputProvider.OnSubmitObservable
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
        }

        #endregion
    }
}
