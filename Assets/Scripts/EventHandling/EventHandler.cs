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

    public StartRoundEvent _startRoundEvent = null;
    public EndRoundEvent _endRoundEvent = null;

    private void Awake()
    {
        if (_startRoundEvent == null)
        {
            _startRoundEvent = new();
        }

        if (_endRoundEvent == null)

        {
            _endRoundEvent = new();
        }
    }
}


