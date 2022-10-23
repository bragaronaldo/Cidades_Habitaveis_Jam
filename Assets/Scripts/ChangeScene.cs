using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public int index;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" || index == 0)
        {
            SceneManager.LoadScene(2);
        }
    }
}
