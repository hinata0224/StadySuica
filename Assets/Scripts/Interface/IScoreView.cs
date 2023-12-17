using System.Collections.Generic;

public interface IScoreView
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
    public void OpenResultPanel(List<int> scoreList);

    /// <summary>
    /// ランキングパネルを表示する
    /// </summary>
    /// <param name="scoreList"></param>
    public void OpenRunkingPanel(List<int> scoreList);

    /// <summary>
    /// パネルを閉じる
    /// </summary>
    public void ClosePanel();
}
