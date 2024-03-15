using UnityEngine.Events;

public class StartRoundEvent : UnityEvent { };
public class EndRoundEvent : UnityEvent<RoundEndCondition> { };
public class PlayerCreatedEvent : UnityEvent<Player> { }
public class DropBlockEvent : UnityEvent { }
public class BlockDiedEvent : UnityEvent { }
