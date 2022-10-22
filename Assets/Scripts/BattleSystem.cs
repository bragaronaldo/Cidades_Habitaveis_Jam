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
    public Text dialogueText;
    Unit playerUnit;
    Unit enemyUnit;
    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;
    public Text playerNameInUI;
    public Text enemyNameInUI;
    private void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }
    IEnumerator SetupBattle()
    {
        GameObject player = Instantiate(playerPrefab, playerArea);
        playerUnit = player.GetComponent<Unit>();
        GameObject enemy = Instantiate(enemyPrefab, enemyArea);
        enemyUnit = enemy.GetComponent<Unit>();

        dialogueText.text = enemyUnit.unitName + " quer batalhar!";

        playerNameInUI.text = playerUnit.unitName;
        enemyNameInUI.text = enemyUnit.unitName;

        playerHUD.SetHUB(playerUnit);
        enemyHUD.SetHUB(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }
    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
        dialogueText.text = "A rima foi efetiva";
        yield return new WaitForSeconds(2f);

        enemyHUD.SetHP(enemyUnit.currentHP);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }
    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.unitName + " Rimou";
        

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHP(playerUnit.currentHP);
        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }
    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "Você ganhou a batalha!";
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "Você perdeu a batalha!";
        }
    }
    void PlayerTurn()
    {
        dialogueText.text = "Escolha uma ação: ";
    }
    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)

            return;
        StartCoroutine(PlayerAttack());
    }
}
