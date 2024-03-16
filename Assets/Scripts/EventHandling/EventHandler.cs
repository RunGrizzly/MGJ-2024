using UnityEngine;
using UnityEngine.Events;

//TODO Kyoooooo move this to your round fool.
public enum RoundEndCondition { Pass, Fail };

public class EventHandler : MonoBehaviour
{
    //A round is  each set of blocks that the player is given
    public class StartRoundEvent : UnityEvent { };

    //Called when the game is ended and we want to pass a pass fail/condition to the leaderboard
    //This  should pass on the round object - for now a game end condition is fine
    public class EndRoundEvent : UnityEvent<RoundEndCondition> { };
    
    public class DropBlockEvent: UnityEvent { }
    public class BlockSettledEvent: UnityEvent<GameObject> {}

    public StartRoundEvent _startRoundEvent = null;
    public EndRoundEvent _endRoundEvent = null;
    public static DropBlockEvent DropBlock;
    public static BlockSettledEvent BlockSettled;

    private void Awake()
    {
        _startRoundEvent ??= new();
        _endRoundEvent ??= new();
        DropBlock ??= new();
        BlockSettled ??= new();
    }
}


