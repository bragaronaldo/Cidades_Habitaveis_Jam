using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] songs;
    private AudioSource _audioSource;
    void Start()
    {
        DontDestroyOnLoad(this);
        _audioSource = GetComponent<AudioSource>();
        _audioSource.Play();
    }
}
