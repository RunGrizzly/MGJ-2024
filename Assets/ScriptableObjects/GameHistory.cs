using System;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class GameHistory : ScriptableObject
    {
        public SerializableDictionary<MedalType, MedalData> Medals;
    }

    [Serializable]
    public class MedalData
    {
        public MedalType MedalType;
        public Player Player;
        public int Score;
    }
}