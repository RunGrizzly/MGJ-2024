using UnityEngine.Events;

public enum MedalType
{
    WobbliestPlayer,
    Effort,
    MostBlocks,
    Waster
}


// public class Medal : UnityEvent<Player> { };

public class MedalEarnedEvent : UnityEvent<Player, MedalType> { };
// public class EffortMedal : Medal { };
// public class MostBlocksMedal : Medal { };
// public class WasterMedal : Medal { };

