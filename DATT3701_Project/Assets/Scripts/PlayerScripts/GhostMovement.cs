using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    // private GameObject playerManager;
    // private PlayerEmotionStatus playerEmotion;
    // private float emotionStatus;
    public float flySpeed = 3.0f;
    public Rigidbody2D rb2d;
    private Vector2 moveInput;

    // Start is called before the first frame update
    void Start()
    {
        // playerManager = GameObject.FindWithTag("PlayerManager");
        // playerEmotion= playerManager.GetComponent<PlayerEmotionStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        //emotionStatus = playerEmotion.getFearStatus();
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();
        rb2d.velocity = moveInput* flySpeed;
        Physics2D.IgnoreLayerCollision(7, 9, true);
        Physics2D.IgnoreLayerCollision(6, 9, true);
        Physics2D.IgnoreLayerCollision(0, 9, true);
    }

    public void Chagenlocation(Vector3 t)
    {
        transform.position = t;
    }

}
