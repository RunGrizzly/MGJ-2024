using UnityEngine;

public class EventHandler : MonoBehaviour
{
    //Game Events
    public RoundCreatedEvent RoundCreatedEvent;
    public StartRoundEvent StartRoundEvent;
    public EndRoundEvent EndRoundEvent;
    public PlayerCreatedEvent PlayerCreatedEvent;
    public DropBlockEvent DropBlockEvent;
    public BlockSettledEvent BlockSettledEvent;

    //Medals
    public MedalEarnedEvent MedalEarnedEvent;

    //Audio
    public PlaySFXEvent PlaySFXEvent;

    private void Awake()
    {
        //Game Events
        RoundCreatedEvent ??= new();
        StartRoundEvent ??= new();
        EndRoundEvent ??= new();
        PlayerCreatedEvent ??= new();
        DropBlockEvent ??= new();
        BlockSettledEvent ??= new();

        //Medals
        MedalEarnedEvent ??= new();

        //Audio
        PlaySFXEvent ??= new();
    }
}