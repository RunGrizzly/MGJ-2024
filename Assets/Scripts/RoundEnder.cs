using System;
using UnityEngine;

public class RoundEnder: MonoBehaviour
{
    private EventHandler _eventHandler;
    private RoundManager _roundManager;
    private void Start()
    {
        _eventHandler = Brain.ins.EventHandler;
        _roundManager = Brain.ins.RoundManager;

        _eventHandler.BlockSettledEvent.AddListener(CheckRoundOver);
    }

    private void CheckRoundOver(Block _)
    {
        var colliders =
            Physics.OverlapBox(
                transform.position,
                new Vector3(100f, 0.1f, 100f),
                Quaternion.identity
            );

        if (colliders.Length <= 0) return;
        
        if (colliders[0].gameObject.layer != 7) return;
        _roundManager.CurrentRound.CompleteRound(RoundState.Pass);
        _eventHandler.EndRoundEvent.Invoke(_roundManager.CurrentRound);
    }
}
