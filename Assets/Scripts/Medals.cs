using UnityEngine;
using UnityEngine.Events;

public enum MedalType
{
    WobbliestPlayer,
    Effort,
    MostBlocks,
    BiggestLoser
}

public class MedalEarnedEvent : UnityEvent<Player, MedalType> { };
