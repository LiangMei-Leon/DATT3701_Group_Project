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

    private AudioManager audioManager;
    private GameObject dialogText;
    private GameObject dialogText2;

    private ParticleSystem juiceVFX1;
	private ParticleSystem juiceVFX2; 

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.FindWithTag("PlayerManager");
        playerEmotion= playerManager.GetComponent<PlayerEmotionStatus>();
        normalPlayer = GameObject.FindWithTag("Player");
        m_Rigidbody2D = normalPlayer.GetComponent<Rigidbody2D>();
        audioManager = FindObjectOfType<AudioManager>();
        dialogText = normalPlayer.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
        dialogText2 = normalPlayer.transform.GetChild(1).GetChild(0).GetChild(1).gameObject;

        juiceVFX1 = normalPlayer.gameObject.transform.GetChild(0).GetChild(2).GetChild(0).gameObject.GetComponent<ParticleSystem>();
		juiceVFX2 = normalPlayer.gameObject.transform.GetChild(0).GetChild(2).GetChild(1).gameObject.GetComponent<ParticleSystem>();
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
        if(col.gameObject.CompareTag("Player") && !playerEmotion.getFearStatus()){
            if(isAbleToHit){
                float random = Random.Range(-10f,10f);
                audioManager.randomVolumeAndPitch("LemonSqueezed");
                audioManager.Play("LemonSqueezed");
                juiceVFX1.Play();
                juiceVFX2.Play();
                playerEmotion.IncreaseRage(IncreaseAmount);
                timer = cooldown;
                if(random >= 0f)
                {
                    if(dialogText != null){
                        dialogText.SetActive(true);
                        Invoke("Cancel", 1f);
                    }
                }else{
                    if(dialogText2 != null){
                        dialogText2.SetActive(true);
                        Invoke("Cancel2", 1f);
                    }
                }
            }
        }
    }

    void Cancel()
    {
        dialogText.SetActive(false);
    }

    void Cancel2()
    {
        dialogText2.SetActive(false);
    }
}
