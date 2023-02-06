using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxFunctions : MonoBehaviour
{
    public float distance_down = 0.425f;
    public float distance_up = 0.5f;
    public float distance_left = 0.5f;
    public LayerMask boxMask;
    public LayerMask playerMask;
    GameObject object1;
    public bool playerNearby = false;
    public bool playerIsUp = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hitPlayerUp = Physics2D.Raycast(transform.position, Vector2.up, distance_up, playerMask);
        if (hitPlayerUp.collider != null){
            playerIsUp = true;
        }else{
            playerIsUp = false;
        }
        RaycastHit2D hitPlayer = Physics2D.Raycast(transform.position, Vector2.left, distance_left, playerMask);
        if (hitPlayer.collider != null){
            playerNearby = true;
        }else{
            playerNearby = false;
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distance_down, boxMask);
        if (hit.collider != null){
            object1 = hit.collider.gameObject;
            //this.GetComponent<Rigidbody2D>().mass = 10;
            if(!playerNearby)
            {
                object1.GetComponent<FixedJoint2D>().enabled = true;
                object1.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
            }else{
                object1.GetComponent<FixedJoint2D>().enabled = false;
            }
            if(playerIsUp)
            {
                object1.GetComponent<FixedJoint2D>().enabled = false;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.down * distance_down);
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.left * distance_left);
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.up * distance_up);
    }
}
