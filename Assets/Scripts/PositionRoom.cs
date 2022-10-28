using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRoom : MonoBehaviour
{

    private GameObject _player;
    void Start()
    {
        _player = GameObject.FindWithTag("Player").gameObject;
        _player.transform.position = this.transform.position;
    }
}
