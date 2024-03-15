using UnityEngine.Events;

public class StartRoundEvent : UnityEvent { };
public class EndRoundEvent : UnityEvent<RoundEndCondition> { };
public class DropBlockEvent : UnityEvent { }
public class BlockDiedEvent : UnityEvent { }
