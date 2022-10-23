using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RhymeHub
{
    [TextArea(3, 10)]
    public string[] critRhymes;
    [TextArea(3, 10)]
    public string[] normalRhymes;
    [TextArea(3, 10)]
    public string[] badRhymes;

}
