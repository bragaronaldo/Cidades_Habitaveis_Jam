using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AfterBattle : MonoBehaviour
{
    private GameObject Player;
    private GameObject Enemy;
    [SerializeField] private player_controller _player;
    private AudioSource _audioSource;
    private AudioManager _audioManager;

    void Start()
    {
        Player = GameObject.FindWithTag("Player").gameObject;
        Enemy = GameObject.FindWithTag("Enemy").gameObject;

        Destroy(Player);
        Destroy(Enemy);

        _audioSource = GameObject.FindWithTag("AudioSource").GetComponent<AudioSource>();
        _audioManager = GameObject.FindWithTag("AudioSource").GetComponent<AudioManager>();

        _audioSource.loop = true;

        if (SceneManager.GetActiveScene().name == "04BatataDepoisDaBatalha")
        {
            _audioSource.clip = _audioManager.songs[0];
            _audioSource.Play();
        }
        // _player = GameObject.FindWithTag("Player").GetComponent<player_controller>();
        // _player.dialogBoxIsOpen = true;
    }
}
