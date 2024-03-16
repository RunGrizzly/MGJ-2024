using UnityEngine;
using UnityEngine.Events;

public class RoundCreatedEvent : UnityEvent<Round> { };
public class StartRoundEvent : UnityEvent<Round> { };
public class EndRoundEvent : UnityEvent<Round> { };
public class PlayerCreatedEvent : UnityEvent<Player> { }
public class DropBlockEvent : UnityEvent { }
public class BlockSettledEvent : UnityEvent<GameObject> { }

//Audio
public class PlaySFXEvent : UnityEvent<AudioClip> { }