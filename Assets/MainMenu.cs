using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private bool _readyToPlay = false;



    private void OnEnable()
    {
        Brain.ins.EventHandler.PlayerCreatedEvent.AddListener(OnPlayerCreated);
        Brain.ins.RoundManager.CurrentRound = new Round();
        _readyToPlay = false;
    }

    private void OnPlayerCreated(Player player)
    {
        _readyToPlay = true;
        Debug.Log("Ready to play!");
    }

    private void Update()
    {
        if (_readyToPlay && Brain.ins.Controls.Player.Everything.WasPressedThisFrame())
        {
            RequestNewRound();
        }
    }

    public void RequestNewRound()
    {

        Brain.ins.SceneHandler.LoadScenes(new List<string>() { "Round", "Leaderboard", "GameScene" });
        Brain.ins.SceneHandler.UnloadScenes(new List<string>() { "MainMenu" });
    }

    private void OnDisable()
    {
        Brain.ins.EventHandler.PlayerCreatedEvent.RemoveListener(OnPlayerCreated);
    }
}
