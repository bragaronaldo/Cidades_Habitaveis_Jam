using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    private GameObject _player;
    private GameObject _enemy;
    private void Start()
    {
        _player = GameObject.FindWithTag("Player").gameObject;
        _enemy = GameObject.FindWithTag("Enemy").gameObject;

        Destroy(_player);
        Destroy(_enemy);
    }
}
