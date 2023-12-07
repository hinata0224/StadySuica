using System.Collections.Generic;
using UnityEngine;

namespace Ball_Data
{
    [CreateAssetMenu(fileName = "BallList", menuName = "SuicaGame/BallList", order = 0)]
    public class BallList : ScriptableObject
    {
        public List<BallData> BallLists = new List<BallData>();
        public List<GameObject> NextBallList = new List<GameObject>();
    }
}
