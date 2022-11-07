using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour
{
    public float charSpeed;
    public GameObject canva;
    private DialogueTrigger trigger;
    // public Vector2 speed;
    public bool blockMovement = false;
    public bool allowDialogBox = false;
    public bool dialogBoxIsOpen = false;
    public LayerMask dialogueLayerMask;
    private Animator animator;
    public Sprite[] sprites;
    public SpriteRenderer spriteRenderer;
    void Start()
    {
        // speed = new Vector2(charSpeed, charSpeed);
        if (canva != null)
        {
            canva = GameObject.FindGameObjectWithTag("DialogueText").gameObject;
        }

        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (blockMovement == true || dialogBoxIsOpen == true)
        {
            // speed = new Vector2(0, 0);
            movementSpeed = 0f;
        }
        else
        {
            MoveCharacterRB();
        }
    }
    void Update()
    {
        if (blockMovement == true || dialogBoxIsOpen == true)
        {
            return;
        }
        else
        {
            ChangeSprite();
        }
    }
    public void ChangeSprite(int i)
    {
        spriteRenderer.sprite = sprites[i];
    }
    public void charMovement()
    {
        // speed = new Vector2(10, 10);
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

        // Vector3 movement = new Vector3(speed.x * inputX, speed.y * inputY, 0);

        // movement *= Time.deltaTime;
        // transform.Translate(movement);

        showDialogueBox();
    }

    private void showDialogueBox()
    {
        if (allowDialogBox == true)
        {
            if (Input.GetKeyDown(KeyCode.E) || (Input.GetKeyDown(KeyCode.Joystick1Button0)))
            {
                trigger.TriggerDialogue();
                dialogBoxIsOpen = true;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (((1 << other.gameObject.layer) & dialogueLayerMask) != 0)
        {
            trigger = other.gameObject.GetComponent<DialogueTrigger>();
            animator = GameObject.FindGameObjectWithTag("Interaction_Animator").GetComponent<Animator>();
            animator.SetBool("interactionOpen", true);

            // speed = new Vector2(0, 0);
            movementSpeed = 0;
            allowDialogBox = true;
            StartCoroutine(charMoveReset());
        }
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
        // speed = new Vector2(10, 10);
        movementSpeed = originalSpeed;

        // charMovement();
    }
    public void PositionZero()
    {
        this.transform.position = new Vector3(0, 0, 0);
    }
    private Rigidbody2D _rb;
    private Vector3 change;
    private float originalSpeed = 6f;
    private float movementSpeed = 6f;
    private void MoveCharacterRB()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        if (change != Vector3.zero)
        {
            _rb.MovePosition(transform.position + change * movementSpeed * Time.fixedDeltaTime);
        }

        if (dialogBoxIsOpen == false)
        {
            movementSpeed = originalSpeed;
        }
    }
    private void ChangeSprite()
    {
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

        showDialogueBox();
    }
}