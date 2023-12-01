using System;
using UniRx;

namespace InputProvider
{
    public interface IInputProvider
    {
        /// <summary>
        /// 決定ボタンの入力イベント
        /// 左クリックを想定
        /// </summary>
        public IObservable<Unit> OnSubmitObservable { get; }
    }
}
