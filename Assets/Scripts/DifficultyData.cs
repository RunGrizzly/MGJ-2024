using System;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public class DifficultyData
    {
        public AnimationCurve SpawnerPositionCurve;
        public AnimationCurve SpawnerSpeedCurve;
        public AnimationCurve SizeScaler;
        public float SeedWidth;
    }
}