using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class RoundOver : MonoBehaviour
{
    private enum State
    {
        Pass,
        Fail
    }

    [SerializeField] private GameObject _doubleUpButton;
    private State _state;

    private void Start()
    {
        if (Brain.ins.RoundManager.CurrentRound.State == RoundState.Pass)
        _state = Brain.ins.RoundManager.CurrentRound.State == RoundState.Pass ? State.Pass : State.Fail;
        if (_state == State.Fail)
        {
            _doubleUpButton.SetActive(false);
        }

        Brain.ins.Controls.Player.Everything.performed += OnEverything;
    }

    private void OnEverything(InputAction.CallbackContext obj)
    {
        if (_state == State.Pass && obj.interaction is MultiTapInteraction)
        {
            Debug.Log("Double this bitch up!!!!");
        }
        else if (obj.interaction is TapInteraction)
        {
            Debug.Log("Go back to menu, bitch!");
        }
    }
}