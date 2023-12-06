using System;
using System.Collections.Generic;
using Constants;

public interface IGameData
{
    /// <summary>
    /// 合成したボールのタイプ
    /// </summary>
    public IObservable<CBallType> ConnectBallType { get; }
    /// <summary>
    /// CBallTypeを通知
    /// </summary>
    /// <param name="type">タイプ</param>
    public void AddScoreType(CBallType type);
    /// <summary>
    /// スコアの追加
    /// </summary>
    /// <param name="score">スコア</param>
    public void SaveScore(int score);
    /// <summary>
    /// スコアのランキングを返す
    /// </summary>
    /// <returns>スコアリスト</returns>
    public List<int> GetScoreRanking();
}
