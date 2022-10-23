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
            spriteRenderer.flipX = false;
        }
        else if (inputX < 0)
        {
            spriteRenderer.flipX = true;
        }

        Debug.Log(inputX);
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
