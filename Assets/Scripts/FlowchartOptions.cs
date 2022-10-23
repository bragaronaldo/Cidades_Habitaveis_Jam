using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlowchartOptions : MonoBehaviour
{
    private Rhymes rhymes;
    public int currentTurn = 1;
    public Text[] rhymingOptions;
    // public 

    private void Start()
    {
        rhymes = GameObject.FindWithTag("Rhymes").GetComponent<Rhymes>();
    }

    public void Combat(int turn)
    {
        if (turn == 1)
        {
            FirstTurn();
        }
        if (turn == 2)
        {
            // SecondTurn();
        }
        if (turn == 3)
        {
            ThirdTurn();
        }
        if (turn == 4)
        {
            FourthTurn();
        }
    }
    private int firstTurnIndex = 0;
    private int secondTurnIndex = 0;
    private void FirstTurn()
    {
        // foreach (string rhyme in rhymes.firstRhymes)
        // {
        //     rhymingOptions[firstTurnIndex].text = rhymes.firstRhymes[firstTurnIndex];
        //     firstTurnIndex++;
        // }
        // rhymingOptions[rhymes.rhymesList[0].index].text = rhymes.rhymesList[rhymes.rhymesList[0].index].rhymes;
        // Debug.Log(rhymes.rhymesList[0].index);
    }
    // private void SecondTurn()
    // {
    //     foreach (string rhyme in rhymes.moreRhymes)
    //     {
    //         rhymingOptions[secondTurnIndex].text = rhymes.moreRhymes[secondTurnIndex];
    //         secondTurnIndex++;
    //     }
    // }
    private void ThirdTurn()
    {

    }
    private void FourthTurn()
    {

    }
}

