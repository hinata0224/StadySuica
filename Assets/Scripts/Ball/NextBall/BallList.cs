using System.Collections.Generic;
using UnityEngine;

namespace Ball_Next
{
    [CreateAssetMenu(fileName = "BallList", menuName = "SuicaGame/BallList", order = 0)]
    public class BallList : ScriptableObject
    {
        public List<GameObject> NextBallList = new List<GameObject>();
    }
}
