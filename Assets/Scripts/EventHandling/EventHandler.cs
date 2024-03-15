using UnityEngine;
using UnityEngine.Events;

//TODO Kyoooooo move this to your round fool.
public enum RoundEndCondition { Pass, Fail };

public class EventHandler : MonoBehaviour
{
    public StartRoundEvent _startRoundEvent = null;
    public EndRoundEvent _endRoundEvent = null;
    public PlayerCreatedEvent _playerCreatedEvent = null;

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

        if (_playerCreatedEvent == null)
        {
            _playerCreatedEvent = new();
        }
    }
}


