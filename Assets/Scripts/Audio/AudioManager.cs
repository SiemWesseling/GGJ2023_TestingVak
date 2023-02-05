using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance { get { return instance; } }

    [SerializeField] private Sound[] sounds;
    bool initialized;

    private void Awake()
    {
        if (instance == null) { instance = this; }
    }
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
            sounds[i]?.audioSource?.Play();
        }
    }
    public void PlaySound(string soundName)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == soundName)
            {
                sounds[i].audioSource?.Play();
                return;
            }
        }
        Debug.LogWarning("There is no sound that goes by the name: " + soundName);
    }
}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
    [Range(0f, 2f)]
    public float pitch;
    [HideInInspector] public AudioSource audioSource;
}