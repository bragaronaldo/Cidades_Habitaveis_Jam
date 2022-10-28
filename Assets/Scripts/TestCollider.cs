using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollider : MonoBehaviour
{
    private Collider2D _collider;
    public string collided;
    void Start()
    {
        GetComponent<Collider2D>();
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        // Debug.Log("Colidiu: " + other.gameObject.name);
        collided = other.gameObject.name;
    }
}
