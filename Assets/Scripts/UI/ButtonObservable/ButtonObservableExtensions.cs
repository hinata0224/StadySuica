using System;
using UniRx;
using UnityEngine.UI;

/// <summary>
/// Button の Observable の拡張クラス
/// </summary>
public static class ButtonObservableExtensions
{
    /// <summary>
    /// ボタン押下 Observable の拡張メソッド
    /// 指定フレームの間連打防止ができる
    /// </summary>
    /// <param name="button"></param>
    /// <param name="throttleFrame">連打防止フレーム数</param>
    /// <returns></returns>
    public static IObservable<Unit> OnButtonObservable(this Button button, int throttleFrame = 5)
    {
        return button.OnClickAsObservable()
            .ThrottleFirstFrame(throttleFrame);
    }
}
