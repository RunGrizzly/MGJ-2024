using UnityEngine.Events;

//A round is  each set of blocks that the player is given
public class StartRoundEvent : UnityEvent { };

//Called when the game is ended and we want ot pass a pass fail/condition to the leaderboard
public class EndRoundEvent : UnityEvent<RoundEndCondition> { };

//Called when a new Player is created;
public class PlayerCreatedEvent : UnityEvent<Player> { }

public class DropBlockEvent: UnityEvent { }

public class BlockDiedEvent: UnityEvent {}
