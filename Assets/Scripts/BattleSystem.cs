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
    public BattleState state;
    private int currentTurn = 1;
    private Text dialogueText;
    private GameObject playerPrefab;
    private Text playerNameInUI;
    private Text PlayerOptions;
    private Transform playerArea;
    Unit playerUnit;
    private BattleHUD playerHUD;
    private GameObject PlayerActionsUI;
    public GameObject PlayerCombatOptions;
    private GameObject enemyPrefab;
    private BattleHUD enemyHUD;
    private Text enemyNameInUI;
    private Transform enemyArea;
    Unit enemyUnit;
    private void Start()
    {

        playerPrefab = GameObject.FindWithTag("Player").gameObject;
        enemyPrefab = GameObject.FindWithTag("Enemy").gameObject;

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
    IEnumerator PlayerAttack(int AttackIndex)
    {
        if (AttackIndex == 0)
        {
            ShowHideUIInBattle();
            bool isDead = enemyUnit.TakeDamage(playerUnit.lowDamage);
            dialogueText.text = "Rima fraca";
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
        if (AttackIndex == 1)
        {
            ShowHideUIInBattle();

            bool isDead = enemyUnit.TakeDamage(playerUnit.mediumDamage);
            dialogueText.text = "Rima média";
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
        if (AttackIndex == 2)
        {
            ShowHideUIInBattle();

            bool isDead = enemyUnit.TakeDamage(playerUnit.highDamage);
            dialogueText.text = "Rima crítica";
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
    }
    IEnumerator EnemyTurn()
    {
        PlayerActionsUI.SetActive(false);
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
    public FlowchartOptions flowchartOptions;
    void PlayerTurn()
    {
        PlayerActionsUI.SetActive(true);
        dialogueText.text = "Escolha uma ação: ";
    }
    public void Rimar()
    {
        currentTurn++;
        PlayerCombatOptions.SetActive(true);
    }
    public void OnAttackButton(int attackIndex)
    {
        if (state != BattleState.PLAYERTURN)

            return;
        StartCoroutine(PlayerAttack(attackIndex));
    }

}
