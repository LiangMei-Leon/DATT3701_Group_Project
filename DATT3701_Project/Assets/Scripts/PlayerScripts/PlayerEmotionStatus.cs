using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEmotionStatus : MonoBehaviour
{
    [SerializeField] public float initialValue = 0.0f;
    [SerializeField] private float emotionStatus;
    [SerializeField] private float fearStatus;
    [SerializeField] GhostMeterUI ghostmeter;    // add ghostMeter UI  jingwei
    public GameObject normalPlayer;
    public GameObject ghostPlayer;
    public Sprite serenitySprite;
    public Sprite rageSprite;
    private SpriteRenderer normalPlayerSprite;
    private CharacterController2D normalPlayerData;
    private GhostMovement ghostPlayerData;
    const float SERENITY_MAX_VALUE =-100.0f;   //change value range:   -100(serenity)-------0-------100(rage)  jingwei
    const float RAGE_MAX_VALUE = 100.0f;       //change value range:   -100(serenity)-------0-------100(rage)
    const float FEAR_MAX_VALUE = 100.0f;

    public MeterUI Needle;

    // Start is called before the first frame update
    void Start()
    {
        emotionStatus = initialValue;
        Needle.setEmo(emotionStatus); // add this to set default value when start game   jingwei
        
        fearStatus = 0f;
        ghostmeter.setFear(fearStatus); // set fear value to ghost bar default   jingwei

        normalPlayerData = normalPlayer.GetComponent<CharacterController2D>();
        ghostPlayerData = ghostPlayer.GetComponent<GhostMovement>();
        normalPlayerSprite = normalPlayer.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //only for testing
        if(Input.GetKeyDown("e"))
        {
            IncreaseRage(10f);
            Debug.Log("Increase Rage for 10");
            Debug.Log("current value: " + emotionStatus);
            Needle.setEmo(emotionStatus);
            
        }
        if(Input.GetKeyDown("q"))
        {
            IncreaseSerenity(10f);
            Debug.Log("Increase Serenity for 10");
            Debug.Log("current value: " + emotionStatus);
            Needle.setEmo(emotionStatus);
        }
        if(Input.GetKeyDown("f"))
        {
            IncreaseFear(10f);
            Debug.Log("Increase Fear for 10");
            Debug.Log("current fear value: " + fearStatus);
            ghostmeter.setFear(fearStatus);   // set ghostmeter value   jingwei
            
        }
        if(emotionStatus <= 0){
            ToSerenityFace();
        }else{
            ToRageFace();
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
        Needle.setEmo(emotionStatus);
    }

    public void IncreaseFear(float value)
    {
        fearStatus += value;
        if(fearStatus >= FEAR_MAX_VALUE){
            normalPlayer.SetActive(false);
            ghostPlayerData.Chagenlocation(normalPlayer.transform.position);
            ghostPlayer.SetActive(true);
        }
    }

    public float getEmotionStatus(){
        return emotionStatus;
    }

    void ToSerenityFace(){
        normalPlayerSprite.sprite = serenitySprite;
    }

    void ToRageFace(){
        normalPlayerSprite.sprite = rageSprite;
    }
}
