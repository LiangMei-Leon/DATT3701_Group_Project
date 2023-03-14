using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSmash : MonoBehaviour
{
    public float distance = 0.45f;
    public LayerMask boxMask;
    GameObject object1;
    private GameObject playerManager;
    private PlayerEmotionStatus playerEmotion;
    private float emotionStatus;
    private CharacterController2D normalPlayerData;
    private PlayerMovement playerInput;
    private float timer = 0f;
    public float smashTime = 0.5f;
    private BoxFunctions boxfunction;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.FindWithTag("PlayerManager");
        playerEmotion= playerManager.GetComponent<PlayerEmotionStatus>();
        normalPlayerData = this.GetComponent<CharacterController2D>();
        playerInput = this.GetComponent<PlayerMovement>();
        _animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        emotionStatus = playerEmotion.getEmotionStatus();
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit;
        if(!normalPlayerData.fliped)
            hit = Physics2D.Raycast(transform.position, Vector2.right, distance, boxMask);
        else
            hit = Physics2D.Raycast(transform.position, Vector2.left, distance, boxMask);
        //push
        if (hit.collider != null && emotionStatus <= 0 && normalPlayerData.jumpable && playerInput.horizontalMove != 0)
        {
            _animator.SetBool("Pushing", true);
        }
        else{
            _animator.SetBool("Pushing", false);
        }
        //smash
        if (hit.collider != null && emotionStatus > 0 && normalPlayerData.jumpable && playerInput.horizontalMove != 0)
        {
            _animator.SetBool("Smashing", true);
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
            _animator.SetBool("Smashing", false);
            timer = 0f;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * distance * Mathf.Sign(transform.localScale.x));
    }
}
