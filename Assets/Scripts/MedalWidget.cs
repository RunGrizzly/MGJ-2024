using TMPro;
using UnityEngine;

public class MedalWidget : MonoBehaviour
{
    [field: SerializeField] public AudioClip MedalSpawnSting { get; set; } = null;
    [field: SerializeField] public AudioClip MedalJingleSting { get; set; } = null;
    [field: SerializeField] public AudioClip MedalWhooshSting { get; set; } = null;
    [field: SerializeField] public TextMeshProUGUI WidgetText { get; set; } = null;

}