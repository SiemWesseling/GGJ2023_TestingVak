using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] Sound musicStart;
    [SerializeField] Sound musicLoop;

    private void Start()
    {
        SetupSound(musicStart);
        SetupSound(musicLoop);
        musicStart.audioSource.Play();
    }
    private void Update()
    {
        if(!musicStart.audioSource.isPlaying && !musicLoop.audioSource.isPlaying)
        {
            musicLoop.audioSource.Play();
        }
    }

    void SetupSound(Sound sound)
    {
        sound.audioSource = transform.AddComponent<AudioSource>();
        sound.audioSource.clip = sound.clip;
        sound.audioSource.pitch = sound.pitch;
        sound.audioSource.volume = sound.volume;
    }
}