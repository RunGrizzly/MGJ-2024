using UnityEngine;
using UnityEngine.Events;

public class RoundCreatedEvent : UnityEvent<Round> { };
public class StartRoundEvent : UnityEvent<Round> { };
public class EndRoundEvent : UnityEvent<Round> { };
public class RoundLostEvent : UnityEvent<Round> { };
public class SessionEndEvent : UnityEvent<Round> { };
public class PlayerCreatedEvent : UnityEvent<Player> { }
public class DropBlockEvent : UnityEvent<Block> { }
public class BlockSettledEvent : UnityEvent<Block> { }
public class SetMusicEvent : UnityEvent<string> { }

//Audio
public class PlaySFXEvent : UnityEvent<AudioClip, float> { }