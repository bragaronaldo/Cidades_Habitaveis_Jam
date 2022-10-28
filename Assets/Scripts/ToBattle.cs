using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToBattle : MonoBehaviour
{
    private BoxCollider2D _boxCollider;
    private player_controller _player;
    private DialogueManager dialogueManager;
    private void Start()
    {
        dialogueManager = GameObject.FindWithTag("Dialog_Manager").GetComponent<DialogueManager>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _player = GameObject.FindWithTag("Player").GetComponent<player_controller>();
    }
    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     _player.blockMovement = true;
    //     SceneManager.LoadScene("BattleSceneSandbox");
    // }
    public void BatataBattleScene()
    {
        if (_player != null)
        {
            // if (dialogueManager.endDialogue == true)
            // {
                _player.blockMovement = true;
                SceneManager.LoadScene("BattleSceneSandbox");
            // }
        }
    }
}
