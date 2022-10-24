using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public enum BattleState
{
    START, PLAYERTURN, ENEMYTURN, WON, LOST
}
public class BattleSystem : MonoBehaviour
{
    public BattleState state;
    private int currentTurn = 1;
    private Text dialogueText;
    private GameObject playerPrefab;
    private Text playerNameInUI;
    private Text PlayerOptions;
    private Transform playerArea;
    Unit playerUnit;
    private player_controller _playerController;
    private BattleHUD playerHUD;
    private GameObject PlayerActionsUI;
    public GameObject PlayerCombatOptions;
    private GameObject enemyPrefab;
    private BattleHUD enemyHUD;
    private Text enemyNameInUI;
    private Transform enemyArea;

    private RhymeTrigger trigger;

    Unit enemyUnit;

    private Enemy enemyA;
    private void Start()
    {
        trigger = FindObjectOfType<RhymeTrigger>();
        enemyA = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
        playerPrefab = GameObject.FindWithTag("Player").gameObject;
        enemyPrefab = GameObject.FindWithTag("Enemy").gameObject;
        _playerController = GameObject.FindWithTag("Player").GetComponent<player_controller>();

        playerHUD = GameObject.FindWithTag("PlayerHUD").GetComponent<BattleHUD>();
        playerArea = GameObject.FindWithTag("PlayerArea").GetComponent<Transform>();
        playerNameInUI = GameObject.FindWithTag("PlayerNameUI").GetComponent<Text>();
        PlayerActionsUI = GameObject.FindWithTag("PlayerActionsUI").gameObject;

        dialogueText = GameObject.FindWithTag("DialogueText").GetComponent<Text>();

        enemyHUD = GameObject.FindWithTag("EnemyHUD").GetComponent<BattleHUD>();
        enemyArea = GameObject.FindWithTag("EnemyArea").GetComponent<Transform>();
        enemyNameInUI = GameObject.FindWithTag("EnemyNameUI").GetComponent<Text>();

        state = BattleState.START;
        PlayerActionsUI.SetActive(false);
        StartCoroutine(SetupBattle());

        enemyFill = GameObject.FindWithTag("EnemyFill").GetComponent<Image>();
        playerFill = GameObject.FindWithTag("PlayerFill").GetComponent<Image>();


        PlayerCombatOptions.SetActive(false);

    }
  
    IEnumerator SetupBattle()
    {
        _playerController.spriteRenderer.sprite = _playerController.sprites[5];
        _playerController.transform.localScale = new Vector3(0.05f,0.05f,0.05f);

        enemyA.ChangeSprite(0);
        // GameObject player = Instantiate(playerPrefab, playerArea);
        GameObject player = playerPrefab;
        player.transform.position = playerArea.transform.position;
        playerUnit = player.GetComponent<Unit>();

        // GameObject enemy = Instantiate(enemyPrefab, enemyArea);
        GameObject enemy = enemyPrefab;
        enemy.transform.position = enemyArea.transform.position;
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
    private float waitingTime = 2.3f;
    private void ShowHideUIInBattle()
    {
        PlayerCombatOptions.SetActive(false);
        PlayerActionsUI.SetActive(false);
    }
    IEnumerator PlayerAttack(GameObject rhyme)
    {   
        string ojbChildrenText = rhyme.GetComponentInChildren<Text>().text;
        int indexInCrit = Array.IndexOf(trigger.rhyme.critRhymes, ojbChildrenText);
        int indexInNormal = Array.IndexOf(trigger.rhyme.normalRhymes, ojbChildrenText);
        int indexInBad = Array.IndexOf(trigger.rhyme.badRhymes, ojbChildrenText);

        if (indexInCrit > -1)
        {
            ShowHideUIInBattle();
            bool isDead = enemyUnit.TakeDamage(playerUnit.highDamage);
            dialogueText.text = trigger.rhyme.critRhymes[indexInCrit];
            yield return new WaitForSeconds(waitingTime);
            dialogueText.text = "Escaldou!";
            yield return new WaitForSeconds(waitingTime);

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

        if (indexInNormal > -1)
        {
            ShowHideUIInBattle();
            bool isDead = enemyUnit.TakeDamage(playerUnit.mediumDamage);
            dialogueText.text = trigger.rhyme.normalRhymes[indexInNormal];
            yield return new WaitForSeconds(waitingTime);
            dialogueText.text = "Pô, deu pro gasto até";
            yield return new WaitForSeconds(waitingTime);

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

        if (indexInBad > -1)
        {
            ShowHideUIInBattle();
            bool isDead = enemyUnit.TakeDamage(playerUnit.lowDamage);
            dialogueText.text = trigger.rhyme.badRhymes[indexInBad];
            yield return new WaitForSeconds(waitingTime);
            dialogueText.text = "Pô, deu mole!";
            yield return new WaitForSeconds(waitingTime);

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
        // if (AttackIndex == 1)
        // {
        //     ShowHideUIInBattle();

        //     bool isDead = enemyUnit.TakeDamage(playerUnit.mediumDamage);
        //     dialogueText.text = "Rima média";
        //     yield return new WaitForSeconds(waitingTime);

        //     enemyHUD.SetHP(enemyUnit.currentHP);

        //     if (isDead)
        //     {
        //         state = BattleState.WON;
        //         EndBattle();
        //     }
        //     else
        //     {
        //         state = BattleState.ENEMYTURN;
        //         StartCoroutine(EnemyTurn());
        //     }
        // }
        // if (AttackIndex == 2)
        // {
        //     ShowHideUIInBattle();

        //     bool isDead = enemyUnit.TakeDamage(playerUnit.highDamage);
        //     dialogueText.text = "Rima crítica";
        //     yield return new WaitForSeconds(waitingTime);

        //     enemyHUD.SetHP(enemyUnit.currentHP);

        //     if (isDead)
        //     {
        //         state = BattleState.WON;
        //         EndBattle();
        //     }
        //     else
        //     {
        //         state = BattleState.ENEMYTURN;
        //         StartCoroutine(EnemyTurn());
        //     }
        // }
    }
    IEnumerator EnemyTurn()
    {
        PlayerActionsUI.SetActive(false);
        _playerController.spriteRenderer.sprite = _playerController.sprites[5];
        enemyA.ChangeSprite(1);


        dialogueText.text = enemyUnit.unitName + " vai rimar!";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHP(playerUnit.currentHP);
        yield return new WaitForSeconds(1f);

        enemyTextAfterAttackin();

        yield return new WaitForSeconds(2.2f);


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
    void enemyTextAfterAttackin()
    {
        dialogueText.text = "Perdeu moral!";
    }
    // Esvaziar a barra de vida
    private Image enemyFill;
    private Image playerFill;
    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "Você ganhou a batalha!";
            enemyFill.color = new Color32(0, 0, 0, 255);
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "Você perdeu a batalha!";
            playerFill.color = new Color32(0, 0, 0, 255);
        }
    }
    void PlayerTurn()
    {
        _playerController.spriteRenderer.sprite = _playerController.sprites[4];
        enemyA.ChangeSprite(0);


        PlayerActionsUI.SetActive(true);
        dialogueText.text = "Escolha uma ação: ";
    }
    public void Rimar()
    {
        currentTurn++;
        PlayerCombatOptions.SetActive(true);
    }
    public void OnAttackButton(GameObject rhyme)
    {
        if (state != BattleState.PLAYERTURN)

            return;
        StartCoroutine(PlayerAttack(rhyme));
    }

}
