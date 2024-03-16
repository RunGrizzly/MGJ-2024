using System;
using System.Collections.Generic;
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
    
    private void OnEnable()
    {
        _state = Brain.ins.RoundManager.CurrentRound.State == RoundState.Pass ? State.Pass : State.Fail;
        
        if (_state == State.Fail)
        {
            _doubleUpButton.SetActive(false);
        }

        Brain.ins.Controls.Player.Everything.performed += OnEverything;
    }

    private void OnDisable()
    {
        Brain.ins.Controls.Player.Everything.performed -= OnEverything;
    }

    private void OnEverything(InputAction.CallbackContext obj)
    {
        if (_state == State.Pass && obj.interaction is MultiTapInteraction)
        {
            Brain.ins.RoundManager.UpTheAnte();
            Brain.ins.SceneHandler.UnloadScenes(new List<Scene>{Scene.RoundOver});
        }
        else if (obj.interaction is TapInteraction)
        {
            Brain.ins.SceneHandler.UnloadScenes(new List<Scene>{Scene.RoundOver});
            Brain.ins.SceneHandler.LoadScenes(new List<Scene>{Scene.MainMenu});
            Brain.ins.RoundManager.SessionOver();
            Debug.Log("Go back to menu, bitch!");
        }
    }
}