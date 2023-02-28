using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemonSqueezer : MonoBehaviour
{
    public float IncreaseAmount = 5f;
    private GameObject playerManager;
    private PlayerEmotionStatus playerEmotion;
    private float emotionStatus;
    private GameObject normalPlayer;
    private Rigidbody2D m_Rigidbody2D;

    public float cooldown = 2f;
    private float timer = 0f;
    private bool isAbleToHit = false;

    public float force = 5f;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.FindWithTag("PlayerManager");
        playerEmotion= playerManager.GetComponent<PlayerEmotionStatus>();
        normalPlayer = GameObject.FindWithTag("Player");
        m_Rigidbody2D = normalPlayer.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0){
            isAbleToHit = true;
        }else{
            isAbleToHit = false;
        }
    }

    // void OnTriggerEnter2D(Collider2D col)
    // {
    //     if(col.gameObject.CompareTag("Player")){
    //         Vector2 direction = (normalPlayer.transform.position - transform.position).normalized;
    //         direction.y = 0f;
    //         direction = direction.normalized;
    //         m_Rigidbody2D.AddForce(direction * force, ForceMode2D.Impulse);
    //         playerEmotion.IncreaseRage(IncreaseAmount);
    //     }
    // }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player")){
            if(isAbleToHit){
                playerEmotion.IncreaseRage(IncreaseAmount);
                timer = cooldown;
                //Debug.Log("hit");
            }
        }
    }
}
