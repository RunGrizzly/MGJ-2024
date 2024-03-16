using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    private void OnEnable()
    {
        Brain.ins.EventHandler.EndRoundEvent.AddListener(SyncLeaderboard);


        Brain.ins.EventHandler.MedalEarnedEvent.AddListener(DisplayMedal);

    }
    private void DisplayMedal(Player player, MedalType medalType)
    {
        Debug.LogFormat("{0} achieved {1}", player.Name, medalType.ToString());
    }

    private void SyncLeaderboard(Round context)
    {
        Debug.LogFormat("Leaderboard received a sync call from an round with the {0} win condition", context.ToString());
    }
}
