using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private TextMeshProUGUI nameText;
    private TextMeshProUGUI dialogueText;
    private Queue<string> sentences;
    private player_controller _player;
    private Queue<string> names;
    private Animator animator;

    private void Awake()
    {
        nameText = GameObject.FindGameObjectWithTag("Dialog_Name").GetComponent<TextMeshProUGUI>();
        dialogueText = GameObject.FindGameObjectWithTag("Dialog_Conversation").GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {
        sentences = new Queue<string>();
        names = new Queue<string>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<player_controller>();
        animator = GameObject.FindGameObjectWithTag("Dialog_Animator").GetComponent<Animator>();


        toBattle = GameObject.FindWithTag("Enemy").GetComponent<ToBattle>();
    }
    public void StartDialogue(Dialogue[] dialogue)
    {
        animator.SetBool("isOpen", true);

        sentences.Clear();
        names.Clear();

        foreach (Dialogue converse in dialogue)
        {
            foreach (string sentence in converse.sentences)
            {
                names.Enqueue(converse.names[0]);
                sentences.Enqueue(sentence);
            }
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        if (names.Count == 0)
        {
            EndDialogue();
            return;
        }

        string name = names.Dequeue();
        string sentence = sentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence, name));
    }


    IEnumerator TypeSentence(string sentence, string name)
    {
        dialogueText.text = "";
        nameText.text = name;

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
    private ToBattle toBattle;
    void EndDialogue()
    {
        animator.SetBool("isOpen", false);
        _player.dialogBoxIsOpen = false;
        _player.allowDialogBox = false;
        // Debug.Log("End Dialogue");
        // if (SceneManager.GetActiveScene().name == "02Sala" || SceneManager.GetActiveScene().name == "03Rua")
        // {
        var other = GameObject.FindWithTag("Player").GetComponent<TestCollider>();
        toBattle.StreetScene(other.collided);
        // }
    }
}
