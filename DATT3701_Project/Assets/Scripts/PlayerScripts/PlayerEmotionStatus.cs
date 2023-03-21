using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerEmotionStatus : MonoBehaviour
{
    [SerializeField] public float initialValue = 0.0f;
    [SerializeField] private float emotionStatus;
    [SerializeField] private float fearStatus;
    [SerializeField] private bool isGhost;
    [SerializeField] public float fearCountDown = 10.0f;
    private float fearTimer = 0f;
    [SerializeField] public bool respawnable = false;
    [SerializeField] public bool respawnUsed = false;
    [SerializeField] GhostMeterUI ghostmeter;    // add ghostMeter UI  jingwei
    public GameObject normalPlayer;
    public GameObject ghostPlayer;
    public Sprite serenitySprite;
    public Sprite rageSprite;
    public GameObject LemonAngel;

    public GameObject Gameover;
    public GameObject pauseShade;
    public GameObject TEXT2;
    public TextMeshProUGUI tmp;
    private SpriteRenderer normalPlayerSprite;
    private CharacterController2D normalPlayerData;
    private GhostMovement ghostPlayerData;
    const float SERENITY_MAX_VALUE =-100.0f;   //change value range:   -100(serenity)-------0-------100(rage)  jingwei
    const float RAGE_MAX_VALUE = 100.0f;       //change value range:   -100(serenity)-------0-------100(rage)
    const float FEAR_MAX_VALUE = 100.0f;

    public MeterUI Needle;

    private Animator _animator;
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        emotionStatus = initialValue;
        _animator = normalPlayer.GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();

        if(Needle != null)
            Needle.setEmo(emotionStatus); // add this to set default value when start game   jingwei
        
        fearStatus = 0f;
        isGhost = false;
        if(ghostmeter != null)
            ghostmeter.setFear(fearStatus); // set fear value to ghost bar default   jingwei

        normalPlayerData = normalPlayer.GetComponent<CharacterController2D>();
        ghostPlayerData = ghostPlayer.GetComponent<GhostMovement>();
        normalPlayerSprite = normalPlayer.GetComponent<SpriteRenderer>();

        //only for playtesting pause and restart
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetFloat("Emotion", emotionStatus);
        if(emotionStatus <= 0 && !audioManager.checkIsPlaying("SerenityTheme")){
            audioManager.Stop("RageTheme");
            audioManager.Play("SerenityTheme");
        }else if(emotionStatus > 0 && !audioManager.checkIsPlaying("RageTheme")){
            audioManager.Stop("SerenityTheme");
            audioManager.Play("RageTheme");
        }
        
        if(Input.GetKeyDown("e"))
        {
            IncreaseRage(10f);
            Debug.Log("Increase Rage for 10");
            Debug.Log("current value: " + emotionStatus);
            if(Needle != null)
                Needle.setEmo(emotionStatus);
        }
        if(Input.GetKeyDown("q"))
        {
            IncreaseSerenity(10f);
            Debug.Log("Increase Serenity for 10");
            Debug.Log("current value: " + emotionStatus);
            if(Needle != null)
                Needle.setEmo(emotionStatus);
        }
        if(Input.GetKeyDown("f"))
        {
            IncreaseFear(10f);
            Debug.Log("Increase Fear for 10");
            Debug.Log("current fear value: " + fearStatus);
            if(ghostmeter != null)
                ghostmeter.setFear(fearStatus);   // set ghostmeter value   jingwei
            
        }
        if(emotionStatus <= 0){
            ToSerenityFace();
        }else{
            ToRageFace();
        }
        if(fearStatus >= 100 && respawnUsed){
            pauseShade.SetActive(true);
            Gameover.SetActive(true);
        }
        if(isGhost && !respawnable){
            fearTimer += Time.deltaTime;
            if(fearTimer >= 1){
                fearCountDown -= 1f;
                tmp.text = fearCountDown.ToString();
                fearTimer = 0;
            }
            if(fearCountDown < 0f && !respawnable){
                TEXT2.SetActive(false);
                pauseShade.SetActive(true);
                Gameover.SetActive(true);
            }
        }
    }

    public void ChangeInitialValue(float changeValue)
    {
        initialValue = changeValue;
    }

    public void IncreaseSerenity(float value)
    {
        emotionStatus -= value;
        if(emotionStatus < SERENITY_MAX_VALUE){
            emotionStatus = SERENITY_MAX_VALUE;
        }
        Debug.Log("Increase Serenity for 10");
        Debug.Log("current value: " + emotionStatus);

        //add setEmo method -- Jingwei
        if(Needle != null)
            Needle.setEmo(emotionStatus);
    }

    public void IncreaseRage(float value)
    {
        emotionStatus += value;
        if(emotionStatus > RAGE_MAX_VALUE){
            emotionStatus = RAGE_MAX_VALUE;
        }
        Debug.Log("Increase Rage for 10");
        Debug.Log("current value: " + emotionStatus);

         //add setEmo method -- Jingwei
        if(Needle != null)
            Needle.setEmo(emotionStatus);
    }

    public void IncreaseFear(float value)
    {
        fearStatus += value;
        if(fearStatus >= 100 && !respawnUsed){
            isGhost = true;
        }
        if(isGhost && !respawnUsed){
            foreach (Collider2D c in normalPlayer.GetComponents<Collider2D>())
            {
                if(c.enabled == true)
                    c.enabled = false;
                else
                    c.enabled = true;
            }
            if(TEXT2 != null)
                TEXT2.SetActive(true);
            LemonAngel.SetActive(true);
            ghostPlayerData.Chagenlocation(normalPlayer.transform.position);
            ghostPlayer.SetActive(true);
        }
        if(ghostmeter != null)
            ghostmeter.setFear(fearStatus);
    }

    public float getEmotionStatus(){
        return emotionStatus;
    }

    public bool getFearStatus(){
        return isGhost;
    }

    void ToSerenityFace(){
        normalPlayerSprite.sprite = serenitySprite;
    }

    void ToRageFace(){
        normalPlayerSprite.sprite = rageSprite;
    }

    public void ReturnNormal(){
        fearStatus = 0;
        respawnable = true;
        isGhost = false;
        respawnUsed = true;
        if(TEXT2 != null)
            TEXT2.SetActive(false);
        foreach (Collider2D c in normalPlayer.GetComponents<Collider2D>())
        {
            if(c.enabled == true)
                c.enabled = false;
            else
                c.enabled = true;
        }
        ghostPlayer.SetActive(false);
    }

}
