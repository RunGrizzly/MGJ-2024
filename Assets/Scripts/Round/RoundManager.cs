using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public Round CurrentRound { get; set; } = null;
    private List<Round> PreviousRounds = new List<Round>();
    private void Start()
    {
        Brain.ins.EventHandler.StartRoundEvent.AddListener(() =>
        {
            CurrentRound = new Round();
        });
        
        Brain.ins.EventHandler.BlockSettledEvent.AddListener(block =>
        {
            CurrentRound.Blocks.Add(block);
        });

        Brain.ins.EventHandler.EndRoundEvent.AddListener(_ =>
        {
            PreviousRounds.Add(CurrentRound);
        });
    }
}
