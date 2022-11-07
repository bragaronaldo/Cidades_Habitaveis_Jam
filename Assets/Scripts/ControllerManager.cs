using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControllerManager : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject[] objs;
    public void changeEvent(int i)
    {
        eventSystem.firstSelectedGameObject = objs[i];
    }
}
