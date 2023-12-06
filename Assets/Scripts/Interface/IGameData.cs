using System.Collections.Generic;

public interface IGameData
{
    /// <summary>
    /// スコアの追加
    /// </summary>
    /// <param name="score">スコア</param>
    public void AddScore(int score);
    /// <summary>
    /// スコアのランキングを返す
    /// </summary>
    /// <returns>スコアリスト</returns>
    public List<int> GetScoreRanking();
}
