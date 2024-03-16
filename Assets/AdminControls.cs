using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class AdminControls : MonoBehaviour
{
    public void KickToMainMenu()
    {
        Brain.ins.EventHandler.EndRoundEvent.Invoke(Brain.ins.RoundManager.CurrentRound);

        Brain.ins.SceneHandler.UnloadScenes(new List<string>() { "Round" });
        Brain.ins.SceneHandler.LoadScenes(new List<string>() { "MainMenu" });
    }

    public void TriggerEffortMedal()
    {
        Brain.ins.EventHandler.MedalEarnedEvent.Invoke(new Player("A fake name"), MedalType.WobbliestPlayer);
    }

}
