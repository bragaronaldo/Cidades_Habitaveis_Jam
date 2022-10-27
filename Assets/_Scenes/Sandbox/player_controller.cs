using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour
{
    public float charSpeed;
    public GameObject canva;
    private DialogueTrigger trigger;

    public Vector2 speed;
    public bool blockMovement = false;
    public bool allowDialogBox = false;

    public bool dialogBoxIsOpen = false;
    private Animator animator;
    public Sprite[] sprites;
    public SpriteRenderer spriteRenderer;
    void Start()
    {
        speed = new Vector2(charSpeed, charSpeed);
        canva = GameObject.FindGameObjectWithTag("DialogueText").gameObject;
    }
    void Update()
    {
        if (blockMovement == true || dialogBoxIsOpen == true)
        {
            speed = new Vector2(0, 0);
        }
        else
        {
            charMovement();
        }
    }

    public void ChangeSprite(int i)
    {
        spriteRenderer.sprite = sprites[i];
    }
    public void charMovement()
    {
        speed = new Vector2(10, 10);
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        if (inputX > 0)
        {
            spriteRenderer.sprite = sprites[0];
        }
        else if (inputX < 0)
        {
            spriteRenderer.sprite = sprites[1];

        }
        else if (inputY > 0)
        {
            spriteRenderer.sprite = sprites[2];
        }
        else if (inputY < 0)
        {
            spriteRenderer.sprite = sprites[3];
        }

        Vector3 movement = new Vector3(speed.x * inputX, speed.y * inputY, 0);

        movement *= Time.deltaTime;
        transform.Translate(movement);

        showDialogueBox();
    }
    private void showDialogueBox()
    {
        if (allowDialogBox == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                trigger = GameObject.FindWithTag("Dialogue_Trigger").GetComponent<DialogueTrigger>();

                trigger.TriggerDialogue();
                dialogBoxIsOpen = true;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        animator = GameObject.FindGameObjectWithTag("Interaction_Animator").GetComponent<Animator>();
        animator.SetBool("interactionOpen", true);
        speed = new Vector2(0, 0);
        allowDialogBox = true;
        StartCoroutine(charMoveReset());
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        animator = GameObject.FindGameObjectWithTag("Interaction_Animator").GetComponent<Animator>();
        animator.SetBool("interactionOpen", false);
        allowDialogBox = false;
    }

    IEnumerator charMoveReset()
    {
        yield return new WaitForSeconds(0.3f);
        speed = new Vector2(10, 10);

        charMovement();
    }
}
