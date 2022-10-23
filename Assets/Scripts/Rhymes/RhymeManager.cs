using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RhymeManager : MonoBehaviour
{
    public Text rhymeText;
    public Text dialogueText;
    private Queue<string> critRhymes;
    private Queue<string> normalRhymes;
    private Queue<string> badRhymes;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        critRhymes = new Queue<string>();
        normalRhymes = new Queue<string>();
        badRhymes = new Queue<string>();
    }

    public void StartDialogue (RhymeHub rhyme)
    {

        critRhymes.Clear();
        normalRhymes.Clear();
        badRhymes.Clear();

        foreach (string rhymeLine in rhyme.critRhymes)
        {
            critRhymes.Enqueue(rhymeLine);
        }

        foreach (string rhymeLine in rhyme.normalRhymes)
        {
            normalRhymes.Enqueue(rhymeLine);
        }

        foreach (string rhymeLine in rhyme.badRhymes)
        {
            badRhymes.Enqueue(rhymeLine);
        }

    }

    // public void DisplayNextSentence ()
    // {
    //     if (rhymes.Count == 0)
    //     {
    //         EndDialogue();
    //         return;
    //     }

    //     string sentence = rhymes.Dequeue();
    //     StopAllCoroutines();
    //     StartCoroutine(TypeSentence(sentence));
    // }


    // IEnumerator TypeSentence (string sentence)
    // {
    //     dialogueText.text = "";
    //     foreach (char letter in sentence.ToCharArray())
    //     {
    //         dialogueText.text += letter;
    //         yield return null;
    //     }
    // }
    // void EndDialogue ()
    // {
    //     animator.SetBool("isOpen", false);
    // }

}
