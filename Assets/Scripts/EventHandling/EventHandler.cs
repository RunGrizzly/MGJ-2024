using UnityEngine;

public class EventHandler : MonoBehaviour
{
    //Game Events
    public StartRoundEvent StartRoundEvent;
    public EndRoundEvent EndRoundEvent;
    public PlayerCreatedEvent PlayerCreatedEvent;
    public DropBlockEvent DropBlockEvent;
    public BlockSettledEvent BlockSettledEvent;

    //Medals
    public MedalEarnedEvent MedalEarnedEvent;
    // public EffortMedal EffortMedal;
    // public MostBlocksMedal MostBlocksMedal;
    // public WasterMedal WasterMedal;

    private void Awake()
    {
        //Game Events
        StartRoundEvent ??= new();
        EndRoundEvent ??= new();
        PlayerCreatedEvent ??= new();
        DropBlockEvent ??= new();
        BlockSettledEvent ??= new();

        //Medals
        MedalEarnedEvent ??= new();
        // EffortMedal ??= new();
        // MostBlocksMedal ??= new();
        // WasterMedal ??= new();
    }
}