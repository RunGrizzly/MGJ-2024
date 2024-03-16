using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public List<Round> AllRounds = new();
    public List<Round> CompletedRounds => AllRounds.Where(round => round.State == RoundState.Pass).ToList();
    public Round CurrentRound { get; set; } = null;

    private void Start()
    {
        Brain.ins.EventHandler.StartRoundEvent.AddListener((_) =>
        {
            CurrentRound = new Round();
        });
        
        Brain.ins.EventHandler.BlockSettledEvent.AddListener(block =>
        {
            CurrentRound.Blocks.Add(block);
        });

        Brain.ins.EventHandler.EndRoundEvent.AddListener(_ =>
        {
            AllRounds.Add(CurrentRound);
        });
    }
    
    public void CreateRound()
    {
        CurrentRound = new Round();
        AllRounds.Add(CurrentRound);
        Brain.ins.EventHandler.RoundCreatedEvent.Invoke(CurrentRound);
    }

    public void StartRound()
    {
        CurrentRound.State = RoundState.InProgress;
        Brain.ins.EventHandler.StartRoundEvent.Invoke(CurrentRound);
    }
}