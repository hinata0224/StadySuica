using System.Collections.Generic;
using UnityEngine;

namespace Ball_Data
{
    [CreateAssetMenu(fileName = "BallList", menuName = "SuicaGame/BallList", order = 0)]
    public class BallList : ScriptableObject
    {
        public List<BallData> NextBallList = new List<BallData>();
    }
}
