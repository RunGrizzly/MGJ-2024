using System.Collections.Generic;
using UnityEngine;

public class NameBandit : MonoBehaviour
{
    [SerializeField] private List<NameBanditSection> _sections;
    private int _chosenSectionCount = 0;
    private List<string> _chosenWords = new();
    private bool nameChosen = false;

    [SerializeField] private List<AudioClip> sectionBlips = new List<AudioClip>();

    private void Update()
    {
        if (nameChosen) return;

        if (Brain.ins.Controls.Player.Everything.WasPressedThisFrame())
        {
            var word = _sections[_chosenSectionCount].Stop();
            _chosenWords.Add(word);

            Brain.ins.EventHandler.PlaySFXEvent.Invoke(sectionBlips[+_chosenSectionCount]);

            _chosenSectionCount += 1;
        }

        if (_chosenSectionCount == _sections.Count)
        {
            nameChosen = true;
            var playerName = string.Join(" ", _chosenWords);
            Brain.ins.RoundManager.CurrentRound.Player = new Player(playerName);
            Debug.LogFormat("Player has chosen name {0}", playerName);
        }
    }
}