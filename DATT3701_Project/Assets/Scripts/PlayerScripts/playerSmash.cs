using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSmash : MonoBehaviour
{
    public float distance = 3f;
    public LayerMask boxMask;
    GameObject object1;
    private GameObject playerManager;
    private PlayerEmotionStatus playerEmotion;
    private float emotionStatus;
    //private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.FindWithTag("PlayerManager");
        playerEmotion= playerManager.GetComponent<PlayerEmotionStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        emotionStatus = playerEmotion.getEmotionStatus();
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, Mathf.Abs(transform.localScale.x) * distance, boxMask);
        if (hit.collider != null && emotionStatus > 100){
            object1 = hit.collider.gameObject;
             Destroy(object1, 0.2f);
            //object1.SetActive(false);
            //timer += Time.deltaTime;
            // if(timer >= 0.1f){
            //     Destroy(object1);
            // }
        }
        // else{
        //     timer = 0f;
        // }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * transform.localScale.x * distance);
    }
}
