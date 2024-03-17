using TMPro;
using UnityEngine;

public class LeaderboardLabel : MonoBehaviour
{
    [field: SerializeField] public TextMeshProUGUI NameBox { get; set; } = null;

    public Round Round;

    private void OnEnable()
    {
        Brain.ins.EventHandler.RoundLostEvent.AddListener(OnRoundLost);
    }

    private void OnRoundLost(Round round)
    {
        if (round == Round)
        {
            Destroy(gameObject);
        }
    }
}