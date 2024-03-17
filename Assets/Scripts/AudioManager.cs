using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _musicSource;
    // [SerializeField] private AudioClip _medalEarnedSting;
    [SerializeField] private SerializableDictionary<string, AudioClip> _musicClips = new();

    private void Start()
    {
        Brain.ins.EventHandler.PlaySFXEvent.AddListener(PlaySFX);
        Brain.ins.EventHandler.SetMusicEvent.AddListener(OnSetMusic);
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

    private AudioClip GetMusicFromID(string ID)
    {
        return (_musicClips[ID]);
    }

    private void OnSetMusic(string musicID)
    {
        _musicSource.Stop();
        _musicSource.clip = GetMusicFromID(musicID);
        _musicSource.Play();
    }


    private void PlaySFX(AudioClip newSFX, float start = 0)
    {
        LeanTween.delayedCall(start, () => _audioSource.PlayOneShot(newSFX));
        //_audioSource.PlayOneShot(newSFX);
    }




}
