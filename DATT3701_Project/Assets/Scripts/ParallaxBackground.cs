using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private GameObject player;
    public float speed = 0f;
    private float x = 0f;
    private float playerPreviousXPosition;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerPreviousXPosition = player.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float playerMovement = player.transform.position.x - playerPreviousXPosition;

        float backgroundMovement = -playerMovement * speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x + backgroundMovement, transform.position.y, transform.position.z);

        playerPreviousXPosition = player.transform.position.x;
    }
}
