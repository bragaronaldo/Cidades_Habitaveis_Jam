using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CNegao : MonoBehaviour
{
    bool inBattle = false;
    void Update()
    {
        BattleScale();
    }
    void BattleScale()
    {
        if (inBattle == false)
        {
            if (SceneManager.GetActiveScene().name == "06BatalhaCNegao")
            {
                transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                inBattle = true;
            }
        }
    }
}
