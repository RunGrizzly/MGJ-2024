using UnityEngine;

public class RoundOver : MonoBehaviour
{
    [SerializeField] private GameObject _doubleUpButton;

    private void Start()
    {
        if (Brain.ins.RoundManager.CurrentRound.State == RoundState.Pass)
        {
            _doubleUpButton.SetActive(false);
        }
    }
}