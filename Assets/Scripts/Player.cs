using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private BoxCollider2D _boxcollider;
    private void Start()
    {
        _boxcollider = GetComponent<BoxCollider2D>();
        DontDestroyOnLoad(this);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            DontDestroyOnLoad(other);
        }
    }

}
