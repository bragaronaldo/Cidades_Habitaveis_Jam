using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterBattle : MonoBehaviour
{
    private GameObject Player;
    private GameObject Enemy;
    [SerializeField] private player_controller _player;

    void Start()
    {
        Player = GameObject.FindWithTag("Player").gameObject;
        Enemy = GameObject.FindWithTag("Enemy").gameObject;

        Destroy(Player);
        Destroy(Enemy);
        // _player = GameObject.FindWithTag("Player").GetComponent<player_controller>();
        // _player.dialogBoxIsOpen = true;

    }
}
