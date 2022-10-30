using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private BoxCollider2D _boxcollider;
    public GameObject iBox;
    private bool box = false;
    private void Start()
    {
        _boxcollider = GetComponent<BoxCollider2D>();
        DontDestroyOnLoad(this);
    }
    void Update()
    {
        if (box == false)
        {
            if (SceneManager.GetActiveScene().name == "02Sala")
            {
                Destroy(iBox);
                box = true;
            }
        }
    }
    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.tag == "Enemy")
    //     {
    //         DontDestroyOnLoad(other.gameObject);
    //     }
    // }
    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.tag == "Enemy")
    //     {
    //         DontDestroyOnLoad(other);
    //     }
    // }

}
