using Constants;
using UniRx;

public interface IScoreModel
{
    public IReadOnlyReactiveProperty<int> RPScore { get; }
    /// <summary>
    /// スコアの加算
    /// </summary>
    /// <param name="type"></param>
    public void AddScore(CBallType type);
    /// <summary>
    /// スコアのセーブ
    /// </summary>
    public void SaveScore();
}
