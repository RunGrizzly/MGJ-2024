using UnityEngine;

public class EventHandler : MonoBehaviour
{
    //Game Events
    public StartRoundEvent StartRoundEvent;
    public EndRoundEvent EndRoundEvent;
    public PlayerCreatedEvent PlayerCreatedEvent;
    public DropBlockEvent DropBlockEvent;
    public BlockDiedEvent BlockDiedEvent;

    //Medals
    public WobbliestMedal WobbliestMedal;
    public EffortMedal EffortMedal;
    public MostBlocksMedal MostBlocksMedal;
    public WasterMedal WasterMedal;

    private void Awake()
    {
        //Game Events
        StartRoundEvent ??= new();
        EndRoundEvent ??= new();
        PlayerCreatedEvent ??= new();
        DropBlock ??= new();
        BlockSettled ??= new();

        //Medals
        WobbliestMedal ??= new();
        EffortMedal ??= new();
        MostBlocksMedal ??= new();
        WasterMedal ??= new();
    }
}