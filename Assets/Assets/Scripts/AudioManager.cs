using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioSource musicPlayer, soundPlayer;

    public AudioClip[] availableSoundClips;
    private Dictionary<string, AudioClip> loadedAudioClips;

    private void Awake()
    {
        
            Instance = this;
    }

    void Start()
    {
        loadedAudioClips = new Dictionary<string, AudioClip>();

        foreach (AudioClip audio in availableSoundClips)
        {
            loadedAudioClips.Add(audio.name, audio);
        }

        musicPlayer.Play();
    }

    public void PlaySound(string name)
    {
        if (loadedAudioClips.ContainsKey(name))
        {
            soundPlayer.Stop(); // Detén cualquier sonido actual
            soundPlayer.clip = loadedAudioClips[name];
            soundPlayer.Play();
        }
       
    }

    public void StopMusic()
    {
        musicPlayer.Stop();
    }
}
