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
        if (round.Ante > _biggestLoss)
        {
            _player = round.Player;
            _biggestLoss = round.Ante;
            
            Brain.ins.EventHandler.MedalEarnedEvent.Invoke(_player, MedalType.BiggestLoser);
        }
    }
}