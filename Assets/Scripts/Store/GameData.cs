using System;
using System.Collections.Generic;
using System.Linq;
using Constants;
using UniRx;

public class GameData : IGameData
{
    private Subject<CBallType> _connectBallType = new Subject<CBallType>();
    public IObservable<CBallType> ConnectBallType => _connectBallType;

    private List<int> _scoreList = new List<int>();

    /// <summary>
    /// CBallTypeを通知
    /// </summary>
    /// <param name="type">タイプ</param>
    public void AddScoreType(CBallType type)
    {
        _connectBallType.OnNext(type);
    }

    public void SaveScore(int score)
    {
        _scoreList.Add(score);
    }

    public List<int> GetScoreRanking()
    {
        _scoreList = _scoreList.OrderByDescending(i => i).ToList();
        return _scoreList;
    }
}
