using System;
using UnityEngine;

namespace Ball_Drop
{
    public class DropController : MonoBehaviour
    {
        [SerializeField] private float _maximumCameraMovableLimit = 5f;
        [SerializeField] private float _speed = 0.02f;

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

            return new Vector2(inputX, 0);
        }
        #endregion

    }
}
