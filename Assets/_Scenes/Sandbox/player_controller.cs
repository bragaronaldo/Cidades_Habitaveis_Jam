using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour
{
    public float charSpeed;
    public GameObject canva;
    public DialogueTrigger trigger;
    Vector2 speed;
    public bool blockMovement = false;
    public Animator animator;
    public Sprite[] sprites;
    public SpriteRenderer spriteRenderer;
    void Start()
    {
        speed = new Vector2(charSpeed, charSpeed);
        canva = GameObject.FindGameObjectWithTag("DialogueText").gameObject;
    }
    void Update()
    {
        if (blockMovement == false)
        {
            charMovement();
        }
    }

    public void charMovement()
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
        
        Vector3 movement = new Vector3(speed.x * inputX, speed.y * inputY, 0);

        movement *= Time.deltaTime;
        transform.Translate(movement);

        showDialogueBox();
    }
    private void showDialogueBox()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            trigger.TriggerDialogue();
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        speed = new Vector2(0, 0);
        StartCoroutine(charMoveReset());
    }

    IEnumerator charMoveReset()
    {
        yield return new WaitForSeconds(0.3f);
        speed = new Vector2(10, 10);

        charMovement();
    }
}
