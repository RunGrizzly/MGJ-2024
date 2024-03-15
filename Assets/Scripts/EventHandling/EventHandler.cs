using UnityEngine;
using UnityEngine.Events;

//TODO Kyoooooo move this to your round fool.
public enum RoundEndCondition { Pass, Fail };

public class EventHandler : MonoBehaviour
{
    public StartRoundEvent _startRoundEvent = null;
    public EndRoundEvent _endRoundEvent = null;
    public PlayerCreatedEvent _playerCreatedEvent = null;
    public DropBlockEvent DropBlock;
    public BlockDiedEvent BlockDied;

    private void Awake()
    {
        _startRoundEvent ??= new();
        _endRoundEvent ??= new();
        DropBlock ??= new();
        BlockDied ??= new();
    }
}


