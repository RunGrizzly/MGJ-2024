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
        if (CompletedRounds.Count > 0)
        {
            var x = CompletedRounds.Last().Blocks.Last().GetHighestPoint().y;
            CurrentRound = new Round { StartHeight = CompletedRounds.Last().Blocks.Last().GetHighestPoint().y };
        }
        else
        {
            CurrentRound = new Round { StartHeight = 0 };
        }
        
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