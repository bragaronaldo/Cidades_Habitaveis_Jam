using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhymeTrigger : MonoBehaviour
{

    public RhymeHub[] rhymes;
    public void TriggerRhyme()
    {
        FindObjectOfType<RhymeManager>().StartRhymes(rhymes);
    }
}
