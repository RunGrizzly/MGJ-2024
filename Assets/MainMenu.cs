using System.Collections.Generic;
using UnityEngine;
public class MainMenu : MonoBehaviour
{
    public void RequestNewRound()
    {
        Brain.ins.SceneHandler.LoadScenes(new List<string>() { "Round", "Leaderboard", "GameScene" });
        Brain.ins.SceneHandler.UnloadScenes(new List<string>() { "MainMenu" });
    }
}
