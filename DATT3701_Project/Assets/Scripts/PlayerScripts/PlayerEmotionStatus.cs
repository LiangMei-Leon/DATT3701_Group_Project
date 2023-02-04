using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEmotionStatus : MonoBehaviour
{
    [SerializeField] public float initialValue = 0.0f;
    [SerializeField] private float emotionStatus;
    [SerializeField] private float fearStatus;
    public GameObject normalPlayer;
    public GameObject ghostPlayer;
    private CharacterController2D normalPlayerData;
    private GhostMovement ghostPlayerData;
    const float SERENITY_MAX_VALUE = 0.0f;
    const float RAGE_MAX_VALUE = 200.0f;
    const float FEAR_MAX_VALUE = 100.0f;

    public MeterUI Needle;

    // Start is called before the first frame update
    void Start()
    {
        emotionStatus = initialValue;
        fearStatus = 0f;
        normalPlayerData = normalPlayer.GetComponent<CharacterController2D>();
        ghostPlayerData = ghostPlayer.GetComponent<GhostMovement>();
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
            Debug.Log("Increase Serenity for 10");
            Debug.Log("current fear value: " + fearStatus);
            
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
    }

    public void IncreaseRage(float value)
    {
        emotionStatus += value;
        if(emotionStatus > RAGE_MAX_VALUE){
            emotionStatus = RAGE_MAX_VALUE;
        }
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
}
