using System;
using UnityEngine;

public class RoundEnder: MonoBehaviour
{
    private void Start()
    {
        Brain.ins.EventHandler.BlockSettledEvent.AddListener(CheckRoundOver);
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
        
        if (colliders[0].gameObject.layer != 6) return;
        Brain.ins.RoundManager.EndRound(true);
    }
}
