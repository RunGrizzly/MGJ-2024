using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class GameHistory : ScriptableObject
    {
        public SerializableDictionary<MedalType, Player> Medals;
    }
}