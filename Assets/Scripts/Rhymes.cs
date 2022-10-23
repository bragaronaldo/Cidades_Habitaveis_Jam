using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhymes : MonoBehaviour
{
    [TextArea]
    // public string rhymes;

    public string[] firstRhymes;
    public string[] moreRhymes;
    // private void Start()
    // {
    // var splitRhymes = rhymes.Split('\n');
    // new List<string>(splitRhymes).ForEach((x) => {
    //     Debug.Log(x);
    // });
    // }
    public List<RhymesList> rhymesList = new List<RhymesList>();
    private void Start()
    {
        rhymesList.Add(new RhymesList(0, "Não tem experiência de rua"));
        rhymesList.Add(new RhymesList(1, "Não tem experiência de rua"));
    }


}
