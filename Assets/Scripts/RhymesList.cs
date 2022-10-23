using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhymesList<T>
{
    // public int index;
    // public string rhymes;
    // public RhymesList(int rhymesIndex, string rhymesString)
    // {
    //     index = rhymesIndex;
    //     rhymes = rhymesString;
    // }
    public T Data { get; set; }
    public RhymesList<T> Parent { get; set; }
    public List<RhymesList<T>> Children { get; set; }
    public int GetHeight()
    {
        int height = 1;
        RhymesList<T> current = this;
        while (current.Parent != null)
        {
            height++;
            current = current.Parent;
        }
        return height;
    }
}
