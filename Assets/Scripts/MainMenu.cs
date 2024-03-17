using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private bool _readyToPlay = false;
    [SerializeField] private TextMeshProUGUI _callToAction = null;



    private void OnEnable()
    {
        Brain.ins.EventHandler.PlayerCreatedEvent.AddListener(OnPlayerCreated);
        Brain.ins.RoundManager.CreateRound();
        _readyToPlay = false;

        _callToAction.text = "What's Your Name Champ?";
    }

    private void OnPlayerCreated(Player player)
    {
        _readyToPlay = true;
        Debug.Log("Ready to play!");
        _callToAction.text = "Ready To Play?";
    }

    private void Update()
    {
        if (_readyToPlay && Brain.ins.Controls.Player.Everything.WasPressedThisFrame())
        {
            Brain.ins.RoundManager.StartRound();
            Brain.ins.SceneHandler.UnloadScenes(new List<Scene> { Scene.MainMenu });
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
