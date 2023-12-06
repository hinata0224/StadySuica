using System;
using Constants;
using UnityEngine;

namespace Ball_Data
{
    [Serializable]
    public class BallData
    {
        [SerializeField] private GameObject _ball;
        public GameObject Ball => _ball;
        [SerializeField] private CBallType _type;
        public CBallType BallType => _type;
        [SerializeField] private int _score;
        public int Score => _score;
    }
}
