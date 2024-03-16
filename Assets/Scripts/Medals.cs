using UnityEngine.Events;

public enum MedalType
{
    WobbliestPlayer,
    Effort,
    MostBlocks,
    Waster
}

public class MedalEarnedEvent : UnityEvent<Player, MedalType> { };
