using System.Collections.Generic;
using System.Linq;

public class GameData : IGameData
{
    private List<int> _scoreList = new List<int>();

    public void AddScore(int score)
    {
        _scoreList.Add(score);
    }

    public List<int> GetScoreRanking()
    {
        _scoreList = _scoreList.OrderByDescending(i => i).ToList();
        return _scoreList;
    }
}
