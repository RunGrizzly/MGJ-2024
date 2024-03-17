using System;
using ScriptableObjects;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public class DifficultyData
    {
        public Curve SpawnerPositionCurve;
        public AnimationCurve SpawnerSpeedCurve;
        public AnimationCurve SizeScaler;
        public float SeedWidth;
    }
}