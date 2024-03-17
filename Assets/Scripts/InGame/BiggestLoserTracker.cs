using UnityEngine;

public class BiggestLoserTracker : MonoBehaviour
{
    private Player _player;
    private int _biggestLoss;

    private void OnEnable()
    {
        Brain.ins.EventHandler.RoundLostEvent.AddListener(OnRoundLost);
    }

    private void OnRoundLost(Round round)
    {
        var r = Mathf.Min(Brain.ins.RoundManager.CompletedRounds.Count, round.Ante);

        if (r > _biggestLoss)
        {
            _player = round.Player;
            _biggestLoss = r;

            Brain.ins.EventHandler.MedalEarnedEvent.Invoke(_player, MedalType.BiggestLoser, _biggestLoss);
        }
    }
}