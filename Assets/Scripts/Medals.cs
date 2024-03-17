using UnityEngine;
using UnityEngine.Events;

public enum MedalType
{
    MostBlocks,
    HighestPointReached,
    BiggestLoser,
    AnteUp,
    Lucky,
}

public class MedalEarnedEvent : UnityEvent<Player, MedalType, int> { };
