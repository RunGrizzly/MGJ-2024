using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public List<Round> AllRounds = new();
    public List<Round> CompletedRounds => AllRounds.Where(round => round.State == RoundState.Pass).ToList();
    public Round CurrentRound { get; set; } = null;
    private int _sessionCount = 0;

    private void Start()
    {
        Brain.ins.EventHandler.DropBlockEvent.AddListener(block =>
        {
            CurrentRound.Blocks.Add(block);
        });
    }

    public void CreateRound()
    {
        var point = CompletedRounds.Count > 0 ? CompletedRounds.Last().Blocks.Last().GetHighestPoint().y : 0;
        var round = new Round(1, _sessionCount)
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

    public void UpTheAnte()
    {
        var point = CurrentRound.Blocks.Last().GetHighestPoint().y;
        var ante = CurrentRound.Ante + 1;
        var round = new Round(ante, _sessionCount)
        {
            StartHeight = point
        };
        CurrentRound = round;
        
        AllRounds.Add(CurrentRound);
        Brain.ins.EventHandler.RoundCreatedEvent.Invoke(CurrentRound);
        StartRound();
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

    public void SessionOver()
    {
        _sessionCount += 1;
    }
}