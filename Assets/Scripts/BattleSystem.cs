using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState
{
    START, PLAYERTURN, ENEMYTURN, WON, LOST
}
public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public Transform playerArea;
    public Transform enemyArea;
    public BattleState state;

    Unit playerUnit;
    Unit EnemyUnit;
    private void Start()
    {
        state = BattleState.START;
        BattleSpawn();
    }
    void BattleSpawn()
    {
        GameObject player = Instantiate(playerPrefab, playerArea);
        GameObject enemy = Instantiate(enemyPrefab, enemyArea);
    }
}
