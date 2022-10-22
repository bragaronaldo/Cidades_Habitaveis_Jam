using UnityEngine;
using UnityEngine.UI;

public class conversation : MonoBehaviour
{
  public GameObject DialoguePanel;
  public Text DialogueText;
  public string[] dialogue;

  protected int index;
  public float wordSpeed;

  public bool playerIsClose;

  public void zeroText()
  {
    DialogueText.text = "";
    index = 0;
    DialoguePanel.SetActive(false);
  }
}
