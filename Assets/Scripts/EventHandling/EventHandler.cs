using UnityEngine;

public class EventHandler : MonoBehaviour
{
    //Game Events
    public RoundCreatedEvent RoundCreatedEvent;
    public StartRoundEvent StartRoundEvent;
    public EndRoundEvent EndRoundEvent;
    public SessionEndEvent SessionEndEvent;
    public RoundLostEvent RoundLostEvent { get; set; }
    public PlayerCreatedEvent PlayerCreatedEvent;
    public DropBlockEvent DropBlockEvent;
    public BlockSettledEvent BlockSettledEvent;
    public SetMusicEvent SetMusicEvent;

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
        RoundLostEvent ??= new();
        SessionEndEvent ??= new();
        PlayerCreatedEvent ??= new();
        DropBlockEvent ??= new();
        BlockSettledEvent ??= new();
        SetMusicEvent ??= new();

        //Medals
        MedalEarnedEvent ??= new();

        //Audio
        PlaySFXEvent ??= new();
    }
}