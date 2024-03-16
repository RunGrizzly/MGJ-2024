using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    // [SerializeField] private AudioClip _medalEarnedSting;


    private void Start()
    {
        Brain.ins.EventHandler.PlaySFXEvent.AddListener(PlaySFX);
        // Brain.ins.EventHandler.MedalEarnedEvent.AddListener(OnMedalEarned);
    }

    public bool IsBusy()
    {
        return _audioSource.isPlaying;
    }

    // private void OnMedalEarned(Player player, MedalType medalType)
    // {
    //     PlaySFX(_medalEarnedSting);
    // }

    private void PlaySFX(AudioClip newSFX, float start = 0)
    {
        LeanTween.delayedCall(start, () => _audioSource.PlayOneShot(newSFX));
        //_audioSource.PlayOneShot(newSFX);
    }
}
