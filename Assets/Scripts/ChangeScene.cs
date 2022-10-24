using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public int index;
    public Animator animator;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" || index == 0)
        {
            StartCoroutine(transition());
        }
    }
    IEnumerator transition()
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(4);
    }
}
