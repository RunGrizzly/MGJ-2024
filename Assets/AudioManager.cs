using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private void Start()
    {
        Brain.ins.EventHandler.PlaySFXEvent.AddListener(PlaySFX);
    }

    private void PlaySFX(AudioClip newSFX)
    {
        _audioSource.clip = newSFX;
        _audioSource.time = 0f;
        _audioSource.Play();
        //_audioSource.PlayOneShot(newSFX);
    }
}
