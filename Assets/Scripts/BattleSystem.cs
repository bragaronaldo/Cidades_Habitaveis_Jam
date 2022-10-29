using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
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

    [SerializeField]private RhymeTrigger trigger;
    private RhymeManager manager;

    private RhymeStructure[] critStructure;
    private RhymeStructure[] normalStructure;
    private RhymeStructure[] badStructure;

    Unit enemyUnit;
    private AudioSource audioSource;
    public AudioClip[] songs;

    private Enemy enemySprite;
    private void Start()
    {
        trigger = FindObjectOfType<RhymeTrigger>();
        manager = FindObjectOfType<RhymeManager>();
        enemySprite = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
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

        audioSource = GameObject.FindWithTag("AudioSource").GetComponent<AudioSource>();
        PlayerCombatOptions.SetActive(false);

        audioSource.clip = songs[0];
        audioSource.Play();
    }

    IEnumerator SetupBattle()
    {
        _playerController.ChangeSprite(5);

        enemySprite.ChangeSprite(2);
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
        var critRhymes = Filter(trigger, "crit");
        var normalRhymes = Filter(trigger, "normal");
        var badRhymes = Filter(trigger, "bad");

        string ojbChildrenText = rhyme.GetComponentInChildren<Text>().text;

        foreach (RhymeStructure rhymeStructure in critRhymes[0].structures)
        {
            if (rhymeStructure.rhymePreview == ojbChildrenText)
            {
                ShowHideUIInBattle();
                bool isDead = enemyUnit.TakeDamage(playerUnit.highDamage);
                dialogueText.text = rhymeStructure.rhymeText;
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
        }
        foreach (RhymeStructure rhymeStructure in normalRhymes[0].structures)
        {
            if (rhymeStructure.rhymePreview == ojbChildrenText)
            {
                ShowHideUIInBattle();
                bool isDead = enemyUnit.TakeDamage(playerUnit.mediumDamage);
                dialogueText.text = rhymeStructure.rhymeText;
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
        }
        foreach (RhymeStructure rhymeStructure in badRhymes[0].structures)
        {
            if (rhymeStructure.rhymePreview == ojbChildrenText)
            {
                ShowHideUIInBattle();
                bool isDead = enemyUnit.TakeDamage(playerUnit.lowDamage);
                dialogueText.text = rhymeStructure.rhymeText;
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
        }
    }
    IEnumerator EnemyTurn()
    {
        PlayerActionsUI.SetActive(false);
        _playerController.ChangeSprite(5);
        enemySprite.ChangeSprite(3);


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
            enemyFill.color = new Color32(0, 0, 0, 255);

            if (enemyPrefab.name == "Batata")
            {
                dialogueText.text = "Você ganhou a batalha!";
                audioSource.clip = songs[1];
                audioSource.loop = false;
                audioSource.Play();
                StartCoroutine(EndGame());
            }



        }
        else if (state == BattleState.LOST)
        {
            playerFill.color = new Color32(0, 0, 0, 255);

            dialogueText.text = "Você perdeu a batalha!";
            audioSource.clip = songs[2];
            audioSource.loop = false;
            audioSource.Play();
            StartCoroutine(EndGame());
        }
    }
    IEnumerator EndGame()
    {
        if (state == BattleState.LOST)
        {
            yield return new WaitForSeconds(songs[2].length + 0.2f);
            SceneManager.LoadScene("EndGame");
        }
        if (state == BattleState.WON)
        {
            if (enemyPrefab.name == "Batata")
            {
                yield return new WaitForSeconds(songs[1].length + 0.4f);
                SceneManager.LoadScene("04BatataDepoisDaBatalha");
            }
        }
    }
    void PlayerTurn()
    {
        _playerController.spriteRenderer.sprite = _playerController.sprites[4];
        enemySprite.ChangeSprite(2);

        PlayerActionsUI.SetActive(true);
        dialogueText.text = "Escolha uma ação: ";
    }
    public void Rimar()
    {
        if (currentTurn == 1)
        {
            currentTurn++;
            PlayerCombatOptions.SetActive(true);
            PlayerActionsUI.SetActive(false);
            trigger.TriggerRhyme();
        }
        else
        {
            currentTurn++;
            PlayerCombatOptions.SetActive(true);
            PlayerActionsUI.SetActive(false);
            manager.DisplayNextSentence();
        }

    }
    public void OnAttackButton(GameObject rhyme)
    {
        if (state != BattleState.PLAYERTURN)

            return;
        StartCoroutine(PlayerAttack(rhyme));
    }

    public RhymeHub[] Filter(RhymeTrigger input, string rhymeType)
    {
        return input.rhymes.Where(c => c.type == rhymeType).ToArray();
    }
}
