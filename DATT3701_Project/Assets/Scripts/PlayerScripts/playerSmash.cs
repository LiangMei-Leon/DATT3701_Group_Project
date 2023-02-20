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
    public GameObject normalPlayer;
    private CharacterController2D normalPlayerData;
    private float timer = 0f;
    public float smashTime = 0.5f;
    private BoxFunctions boxfunction;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.FindWithTag("PlayerManager");
        playerEmotion= playerManager.GetComponent<PlayerEmotionStatus>();
        normalPlayerData = normalPlayer.GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void Update()
    {
        emotionStatus = playerEmotion.getEmotionStatus();
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, Mathf.Abs(transform.localScale.x) * distance, boxMask);
        if (hit.collider != null && emotionStatus > 0 && normalPlayerData.jumpable){  // change 100 to 0--- jingwei
            object1 = hit.collider.gameObject;
            boxfunction = object1.GetComponent<BoxFunctions>();
            //Destroy(object1, 0.2f);
            if(boxfunction != null){
                timer += Time.deltaTime;
                if(timer >= smashTime){
                    timer = 0f;
                    boxfunction.Smash();
                }
            }
        }
        else{
            timer = 0f;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * transform.localScale.x * distance);
    }
}
