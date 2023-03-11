using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleScript : MonoBehaviour
{
    public float movingSpeed = 2f;
    public bool facingLeft = true;
    private bool obstacleAhead = false;
    public LayerMask Mask;
    public float gizmosLength = 0.4f;
    private SpriteRenderer turtleSprite;

    private GameObject playerManager;
    private PlayerEmotionStatus playerEmotion;
    private float emotionStatus;



    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.FindWithTag("PlayerManager");
        playerEmotion= playerManager.GetComponent<PlayerEmotionStatus>();
        turtleSprite = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        emotionStatus = playerEmotion.getEmotionStatus();
        if(obstacleAhead){
            Flip();
            facingLeft = !facingLeft;
        }

        if(facingLeft){
            RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, gizmosLength, Mask);
            if(hitLeft.collider != null){
                obstacleAhead = true;
            }else{
                obstacleAhead = false;
            }
        }
        if(!facingLeft){
            RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, gizmosLength + 0.05f, Mask);
            if(hitRight.collider != null){
                obstacleAhead = true;
            }else{
                obstacleAhead = false;
            }
        }
        
    }

    void FixedUpdate()
    {
        if(!obstacleAhead && emotionStatus <= 0){
            if(facingLeft){
                transform.Translate(Vector2.left * movingSpeed * Time.deltaTime);
            }else{
                transform.Translate(Vector2.right * movingSpeed * Time.deltaTime);
            }
        }
    }

    private void Flip()
	{
        if(turtleSprite.flipX == false)
		    turtleSprite.flipX = true;
        else
            turtleSprite.flipX = false;
	}


    void OnDrawGizmos()
    {
        // Gizmos.color = Color.red;
        // Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.down * distance_down);
        // Gizmos.DrawLine((Vector2)transform.position + Vector2.down * 0.3f, (Vector2)transform.position + Vector2.left * distance_left + Vector2.down * 0.3f);
        // Gizmos.DrawLine((Vector2)transform.position + Vector2.down * 0.3f, (Vector2)transform.position + Vector2.right * distance_right + Vector2.down * 0.3f);
        Gizmos.color = Color.green;
        if(facingLeft)
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.left * gizmosLength);
        else
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * (gizmosLength + 0.05f));
    }
}
