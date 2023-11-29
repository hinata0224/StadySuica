using System;
using UniRx;
using UnityEngine;

namespace InputProvider
{
    public class UnityInputProvider : SingletonMonoBehaviour<UnityInputProvider>, IInputProvider
    {
        private Subject<Unit> _onSubmitObservable = new Subject<Unit>();

        /// <summary>
        /// 決定ボタンの入力イベント
        /// 連打防止のため 5 フレームの間連打されても一回押されたことにする
        /// フレーム数は適当
        /// </summary>
        public IObservable<Unit> OnSubmitObservable => _onSubmitObservable.ThrottleFirstFrame(5);

        private void Update()
        {
            HandlePrimaryButton();
        }

        private void HandlePrimaryButton()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _onSubmitObservable.OnNext(Unit.Default);
            }
        }
    }
}
