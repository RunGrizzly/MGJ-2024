using UnityEngine;
using UnityEngine.Events;

//TODO Kyoooooo move this to your round fool.
public enum RoundEndCondition { Pass, Fail };

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
        DropBlockEvent ??= new();
        BlockDiedEvent ??= new();

        //Medals
        WobbliestMedal ??= new();
        EffortMedal ??= new();
        MostBlocksMedal ??= new();
        WasterMedal ??= new();
    }
}


