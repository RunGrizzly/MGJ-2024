using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scene
{
    MainMenu,
    Leaderboard,
    GameScene,
    RoundOver,
}

public class SceneHandler : MonoBehaviour
{
    private void Start()
    {
        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }

        LoadScenes(new List<Scene> { Scene.MainMenu, Scene.GameScene, Scene.Leaderboard });

        Brain.ins.EventHandler.EndRoundEvent.AddListener((context) =>
        {
            Debug.LogFormat("The round ended with the {0} condition", context.ToString());
        });
    }

    public void LoadScenes(List<string> sceneNames)
    {
        foreach (string sceneName in sceneNames)
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }
    }

    public void LoadScenes(List<Scene> sceneNames) =>
        LoadScenes(sceneNames.Select(name => Enum.GetName(typeof(Scene), name)).ToList());

    public void UnloadScenes(List<string> sceneNames)
    {
        foreach (string sceneName in sceneNames)
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }
    }

    public void UnloadScenes(List<Scene> sceneNames) =>
        UnloadScenes(sceneNames.Select(name => Enum.GetName(typeof(Scene), name)).ToList());
}