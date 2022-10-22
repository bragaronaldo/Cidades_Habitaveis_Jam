using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToBattle : MonoBehaviour
{
    private BoxCollider2D _boxCollider;
    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        // DontDestroyOnLoad(transform.root.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // DontDestroyOnLoad(this);
        SceneManager.LoadScene("Battle");
    }
}
