using UnityEngine;

public interface IDorpController
{
    /// <summary>
    /// 次のオブジェクトをセット
    /// </summary>
    /// <param name="ball">次のオブジェクト</param>
    public void SetNextBall(GameObject ball);
}
