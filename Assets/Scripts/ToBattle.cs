using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToBattle : MonoBehaviour
{
    private BoxCollider2D _boxCollider;
    private player_controller _player;
    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _player = GameObject.FindWithTag("Player").GetComponent<player_controller>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        _player.blockMovement = true;
        // SceneManager.LoadScene("BattleScene");
        SceneManager.LoadScene("BattleSceneSandbox");
    }
}
