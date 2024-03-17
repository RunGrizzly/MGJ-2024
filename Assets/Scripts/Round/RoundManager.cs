using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public List<Round> AllRounds = new();
    public List<Round> CompletedRounds => AllRounds.Where(round => round.State == RoundState.Pass).ToList();
    public Round CurrentRound { get; set; } = null;
    private int _sessionCount = 0;

    [SerializeField] private AudioClip _startRoundSound = null;
    [SerializeField] private AudioClip _failRoundSound = null;
    [SerializeField] private AudioClip _passRoundSound = null;
    [SerializeField] private AudioClip _anteUpSound = null;

    private void Start()
    {
        Brain.ins.EventHandler.DropBlockEvent.AddListener(block => { CurrentRound.Blocks.Add(block); });
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
        Brain.ins.EventHandler.SetMusicEvent.Invoke("game");

        if (_startRoundSound != null)
        {
            Brain.ins.EventHandler.PlaySFXEvent.Invoke(_startRoundSound, 0);
        }
    }

    public void UpTheAnte()
    {

        if (_anteUpSound != null)
        {
            Brain.ins.EventHandler.PlaySFXEvent.Invoke(_anteUpSound, 0);
        }

        var point = CurrentRound.Blocks.Last().GetHighestPoint().y;
        var ante = CurrentRound.Ante + 1;
        var round = new Round(ante, _sessionCount)
        {
            StartHeight = point,
            Player = CurrentRound.Player
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
            if (_passRoundSound != null)
            {
                Brain.ins.EventHandler.PlaySFXEvent.Invoke(_passRoundSound, 0);
            }
        }
        else
        {
            if (_failRoundSound != null)
            {
                Brain.ins.EventHandler.PlaySFXEvent.Invoke(_failRoundSound, 0);
            }


            CurrentRound.DestroyBlocks();
            var rounds = CompletedRounds.Where(r => r.Session == CurrentRound.Session);
            foreach (var round in rounds)
            {
                round.DestroyBlocks();
                round.State = RoundState.Fail;
                Brain.ins.EventHandler.RoundLostEvent.Invoke(CurrentRound);
            }

            if (CurrentRound.Ante >= 2)
            {
                for (int i = 1; i <= Math.Min(CurrentRound.Ante, CompletedRounds.Count); i++)
                {
                    var lastRound = CompletedRounds.Last();
                    lastRound.DestroyBlocks();
                    lastRound.State = RoundState.Lost;
                    Brain.ins.EventHandler.RoundLostEvent.Invoke(CurrentRound);
                }
            }
        }

        Brain.ins.SceneHandler.LoadScenes(new List<Scene> { Scene.RoundOver });
        Brain.ins.EventHandler.EndRoundEvent.Invoke(CurrentRound);
        Brain.ins.EventHandler.SetMusicEvent.Invoke("menu");
    }

    public void SessionOver()
    {
        Brain.ins.EventHandler.SessionEndEvent.Invoke(CurrentRound);
        _sessionCount += 1;
    }
}