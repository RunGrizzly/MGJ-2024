using System;
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
        Brain.ins.EventHandler.BlockSettledEvent.AddListener(block =>
        {
            CurrentRound.Blocks.Add(block);
        });
    }

    public void CreateRound()
    {
        var point = CompletedRounds.Count > 0 ? CompletedRounds.Last().Blocks.Last().GetHighestPoint().y : 0;
        var ante = CurrentRound == null ? 1 : CurrentRound.Ante + 1;
        var round = new Round(ante)
        {
            StartHeight = point
        };
        CurrentRound = round;
        
        AllRounds.Add(CurrentRound);
        Brain.ins.EventHandler.RoundCreatedEvent.Invoke(CurrentRound);
    }

    public void StartRound()
    {
        CurrentRound.State = RoundState.InProgress;
        Brain.ins.EventHandler.StartRoundEvent.Invoke(CurrentRound);
    }

    public void EndRound(bool hasPassed)
    {
        CurrentRound.State = hasPassed ? RoundState.Pass : RoundState.Fail;
        AllRounds.Add(CurrentRound);
        
        if (hasPassed)
        {
            CurrentRound.FreezeBlocks();
        }
        else
        {
            CurrentRound.DestroyBlocks();
        }
        
        Brain.ins.SceneHandler.LoadScenes(new List<Scene>{ Scene.RoundOver });
        Brain.ins.EventHandler.EndRoundEvent.Invoke(CurrentRound);
    }
}