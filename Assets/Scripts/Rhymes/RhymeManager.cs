using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RhymeManager : MonoBehaviour
{
    public Text rhymeOp1;
    public Text rhymeOp2;
    public Text rhymeOp3;
    public Text rhymeOp4;

    [SerializeField] private List<Text> rhymeOptions;
    private List<string> rhymesPerTurn;
    public Text dialogueText;
    public Text choosenRhymeBox;
    private Queue<string> critRhymes;
    private Queue<string> normalRhymes;
    private Queue<string> badRhymes;
    public Animator animator;

    public int i;
    void Start()
    {
        critRhymes = new Queue<string>();
        normalRhymes = new Queue<string>();
        badRhymes = new Queue<string>();
        rhymeOptions = new List<Text>();
        rhymesPerTurn = new List<string>();
    }

    public void StartRhymes (RhymeHub[] rhyme)
    {
        
        critRhymes.Clear();
        normalRhymes.Clear();
        badRhymes.Clear();

        foreach (RhymeHub rhymeObj in rhyme)
        {
            if (rhymeObj.type == "crit")
            {
            foreach (RhymeStructure rhymeLine in rhymeObj.structures)
                {
                    critRhymes.Enqueue(rhymeLine.rhymePreview);
                }
            }

            if (rhymeObj.type == "normal")
            {
            foreach (RhymeStructure rhymeLine in rhymeObj.structures)
                {
                    normalRhymes.Enqueue(rhymeLine.rhymePreview);
                }
            }

            if (rhymeObj.type == "bad")
            {
            foreach (RhymeStructure rhymeLine in rhymeObj.structures)
                {
                    badRhymes.Enqueue(rhymeLine.rhymePreview);
                }
            }
        }

        DisplayNextSentence();

    }

    public void DisplayNextSentence ()
    {

        string critRhyme = critRhymes.Dequeue();
        string normalRhyme = normalRhymes.Dequeue();
        string normalRhyme2 = normalRhymes.Dequeue();
        string badRhyme = badRhymes.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeRhymes(critRhyme, normalRhyme, normalRhyme2, badRhyme));
    }


    IEnumerator TypeRhymes (string critRhyme, string normalRhyme, string normalRhyme2, string badRhyme)
    {
        rhymeOptions.Clear();
        rhymeOp1.text = "";
        rhymeOp2.text = "";
        rhymeOp3.text = "";
        rhymeOp4.text = "";
        rhymeOptions.Add(rhymeOp1);
        rhymeOptions.Add(rhymeOp2);
        rhymeOptions.Add(rhymeOp3);
        rhymeOptions.Add(rhymeOp4);

        rhymesPerTurn.Add(critRhyme);
        rhymesPerTurn.Add(normalRhyme);
        rhymesPerTurn.Add(normalRhyme2);
        rhymesPerTurn.Add(badRhyme);

        foreach(string rhyme in rhymesPerTurn)
        {
            // Debug.Log(i);
            i = Random.Range(0, rhymeOptions.Count);
            rhymeOptions[i].text = rhyme;
            rhymeOptions.RemoveAt(i);
            yield return null;
        }


        // foreach (char letter in normalRhyme.ToCharArray())
        // {
        //     i = Random.Range(0, rhymeOptions.Count - 1);
        //     rhymeOptions[i].text += letter;
        //     rhymeOptions.RemoveAt(i);
        //     yield return null;
        // }

        // foreach (char letter in normalRhyme2.ToCharArray())
        // {
        //     i = Random.Range(0, rhymeOptions.Count - 1);
        //     rhymeOptions[i].text += letter;
        //     rhymeOptions.RemoveAt(i);
        //     yield return null;
        // }

        // foreach (char letter in badRhyme.ToCharArray())
        // {
        //     rhymeOptions[0].text += letter;
        //     yield return null;
        // }
    }
}
