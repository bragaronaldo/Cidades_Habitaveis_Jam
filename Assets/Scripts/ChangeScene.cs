using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    public int index;
    public Animator animator;
    private player_controller _player;
    void Start()
    {
        if (_player != null)
        {
            _player = GameObject.FindWithTag("Player").GetComponent<player_controller>();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" || index == 0)
        {
            SceneManager.LoadScene("02Sala");
            // StartCoroutine(transition());
        }
        if (SceneManager.GetActiveScene().name == "02Sala")
        {
            SceneManager.LoadScene("03Rua");
            // StartCoroutine(transition());
        }
    }
    IEnumerator transition()
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Play");
    }
    public void changeOnClick(string scene)
    {
        // StartCoroutine(transition());
        if (scene == "Play")
        {
            // StartCoroutine(Fade(FadeDirection.Out));
            SceneManager.LoadScene("01Quarto");
            // _player.PositionZero();
        }
        if (scene == "Creditos")
        {
            SceneManager.LoadScene("10Creditos");
        }
        if (scene == "Sair")
        {
            Application.Quit();
        }
    }

    // Fade

    // #region FIELDS
    // public Image fadeOutUIImage;
    // public float fadeSpeed = 0.8f;

    // public enum FadeDirection
    // {
    //     In, //Alpha = 1
    //     Out // Alpha = 0
    // }
    // #endregion

    // #region MONOBHEAVIOR
    // // void OnEnable()
    // // {
    // //     StartCoroutine(Fade(FadeDirection.Out));
    // // }
    // #endregion

    // #region FADE
    // private IEnumerator Fade(FadeDirection fadeDirection)
    // {
    //     float alpha = (fadeDirection == FadeDirection.Out) ? 1 : 0;
    //     float fadeEndValue = (fadeDirection == FadeDirection.Out) ? 0 : 1;
    //     if (fadeDirection == FadeDirection.Out)
    //     {
    //         while (alpha >= fadeEndValue)
    //         {
    //             SetColorImage(ref alpha, fadeDirection);
    //             yield return null;
    //         }
    //         fadeOutUIImage.enabled = false;
    //     }
    //     else
    //     {
    //         fadeOutUIImage.enabled = true;
    //         while (alpha <= fadeEndValue)
    //         {
    //             SetColorImage(ref alpha, fadeDirection);
    //             yield return null;
    //         }
    //     }
    // }
    // #endregion

    // #region HELPERS
    // public IEnumerator FadeAndLoadScene(FadeDirection fadeDirection, string sceneToLoad)
    // {
    //     yield return Fade(fadeDirection);
    //     SceneManager.LoadScene(sceneToLoad);
    // }

    // private void SetColorImage(ref float alpha, FadeDirection fadeDirection)
    // {
    //     fadeOutUIImage.color = new Color(fadeOutUIImage.color.r, fadeOutUIImage.color.g, fadeOutUIImage.color.b, alpha);
    //     alpha += Time.deltaTime * (1.0f / fadeSpeed) * ((fadeDirection == FadeDirection.Out) ? -1 : 1);
    // }
    // #endregion
}
