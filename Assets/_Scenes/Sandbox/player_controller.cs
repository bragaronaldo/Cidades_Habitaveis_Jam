using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour
{
    public float charSpeed;
    
    Vector2 speed;
    // Start is called before the first frame update
    void Start()
    {
    speed = new Vector2(charSpeed, charSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        charMovement();

    }

    public void charMovement()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(speed.x * inputX, speed.y * inputY, 0);

        movement *= Time.deltaTime;
        transform.Translate(movement);
    }
}
