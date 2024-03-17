using System.Collections.Generic;
using UnityEngine;

public class NameBandit : MonoBehaviour
{
    [SerializeField] private List<NameBanditSection> _sections;
    private int _chosenSectionCount = 0;
    private List<string> _chosenWords = new();
    private bool nameChosen = false;

    [SerializeField] private List<AudioClip> sectionBlips = new List<AudioClip>();
    [SerializeField] private RectTransform _buttonUITemplate = null;
    private RectTransform _buttonUIInstance = null;

    private int buttonMoveTween = -99;

    private void Start()
    {
        // if (_buttonUIInstance != null)
        // {
        //     Destroy(_buttonUIInstance.gameObject);
        // }

        _buttonUIInstance = Instantiate(_buttonUITemplate, transform);

        _buttonUIInstance.anchoredPosition = _sections[0].RectTransform.anchoredPosition;
    }

    private void Update()
    {
        if (nameChosen) return;

        if (Brain.ins.Controls.Player.Everything.WasPressedThisFrame())
        {

            var word = _sections[_chosenSectionCount].Stop();
            _chosenWords.Add(word);

            Brain.ins.EventHandler.PlaySFXEvent.Invoke(sectionBlips[+_chosenSectionCount], 0);

            _chosenSectionCount += 1;



            if (_chosenSectionCount < _sections.Count)
            {
                var newTargetPosition = _sections[_chosenSectionCount].RectTransform.anchoredPosition;

                // /newTargetPosition.x /= 2;
                newTargetPosition.y -= 250f;


                LeanTween.cancel(buttonMoveTween);

                buttonMoveTween = LeanTween.move(_buttonUIInstance, newTargetPosition, 0.35f).setEase(LeanTweenType.easeOutExpo).id;


                //    / _buttonUIInstance.anchoredPosition = newTargetPosition;
            }

            else
            {

                var newTargetPosition = _sections[_sections.Count / 2].RectTransform.anchoredPosition;

                // /newTargetPosition.x /= 2;
                newTargetPosition.y -= 250f;

                LeanTween.cancel(buttonMoveTween);

                buttonMoveTween = LeanTween.move(_buttonUIInstance, newTargetPosition, 0.35f).setEase(LeanTweenType.easeOutExpo).id;

                // /_buttonUIInstance.anchoredPosition = newTargetPosition;


                //Destroy(_buttonUIInstance.gameObject);
            }
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