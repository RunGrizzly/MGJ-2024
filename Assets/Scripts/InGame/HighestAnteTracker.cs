using UnityEngine;

namespace DefaultNamespace
{
    public class HighestAnteTracker : MonoBehaviour
    {
        private int _highestAnte = 0;
        private Player _player;

        private void OnEnable()
        {
            Brain.ins.EventHandler.EndRoundEvent.AddListener(OnRoundEnd);
        }

        private void OnRoundEnd(Round round)
        {
            if (round.Ante > _highestAnte)
            {
                _highestAnte = round.Ante;
                _player = round.Player;
                Brain.ins.EventHandler.MedalEarnedEvent.Invoke(_player, MedalType.AnteUp, _highestAnte);
            }
        }
    }
}