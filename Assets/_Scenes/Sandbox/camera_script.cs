using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_script : MonoBehaviour
{
    public GameObject player;
    private Vector3 playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        // player = GameObject.FindWithTag("Player").GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        moveCamera();
    }

    public void moveCamera()
    {
        playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        transform.position = playerPosition;
    }
}
