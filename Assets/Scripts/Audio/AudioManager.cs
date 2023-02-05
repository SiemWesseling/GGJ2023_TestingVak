using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;
    bool initialized;

    private void Start()
    {
        InitSounds();
    }
    void InitSounds()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].audioSource = transform.AddComponent<AudioSource>();
            sounds[i].audioSource.clip = sounds[i].clip;
            sounds[i].volume = sounds[i].volume;
            sounds[i].pitch = sounds[i].pitch;
        }
        initialized = true;
    }

    public void PlaySound(int i)
    {
        if (initialized)
        {
            sounds[i].audioSource?.Play();
        }
    }
}

[System.Serializable]
public class Sound
{
    public AudioClip clip;
    public float volume;
    public float pitch;
    [HideInInspector] public AudioSource audioSource;
}