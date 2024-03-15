using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{

    private void OnEnable()
    {
        Brain.ins.EventHandler._endRoundEvent.AddListener(SyncLeaderboard);

    }

    private void SyncLeaderboard(RoundEndCondition context)
    {
        Debug.LogFormat("Leaderboard received a sync call from an round with the {0} win condition");
    }


}
