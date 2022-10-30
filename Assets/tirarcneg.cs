using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tirarcneg : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject enemy;
    void Start()
    {
        enemy = GameObject.FindWithTag("Enemy").gameObject;
       enemy.SetActive(false);
    }
}
