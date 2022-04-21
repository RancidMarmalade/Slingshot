using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioScript : MonoBehaviour
{
    public AudioSource backgroundNoise;
    public AudioSource _audioSource;
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        
    }

    public void PlayMusic()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();

        if (backgroundNoise.isPlaying) return;
        backgroundNoise.Play();
    }

    public void StopMusic()
    {
        _audioSource.Stop();
        backgroundNoise.Stop();
    }
}
