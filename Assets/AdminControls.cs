using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminControls : MonoBehaviour
{
    public void KickToMainMenu()
    {

        Brain.ins.EventHandler._endRoundEvent.Invoke(GameEndCondition.Pass);


        Brain.ins.SceneHandler.UnloadScenes(new List<string>() { "Round", "Leaderboard", "GameScene" });
        Brain.ins.SceneHandler.LoadScenes(new List<string>() { "MainMenu" });
    }
}
