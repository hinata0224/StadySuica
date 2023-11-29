using System;
using UniRx;
using UnityEngine;

namespace InputProvider
{
    public class UnityInputProvider : SingletonMonoBehaviour<UnityInputProvider>, IInputProvider
    {
        private Subject<Unit> _onSubmitObservable = new Subject<Unit>();

        public IObservable<Unit> OnSubmitObservable => _onSubmitObservable;

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
