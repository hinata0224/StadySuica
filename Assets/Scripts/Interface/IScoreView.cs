using System.Collections.Generic;

public interface IScoreVire
{
    /// <summary>
    /// ゲーム中スコアを表示する
    /// </summary>
    /// <param name="score">スコア</param>
    public void SetScore(int score);

    /// <summary>
    /// リザルトパネルを表示する
    /// </summary>
    /// <param name="scoreList">ランキング用リスト</param>
    /// <param name="score">今回のスコア</param>
    public void OpenResultPanel(List<int> scoreList, int score);
}
