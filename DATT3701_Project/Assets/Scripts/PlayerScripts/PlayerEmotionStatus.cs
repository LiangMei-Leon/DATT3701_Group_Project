using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEmotionStatus : MonoBehaviour
{
    [SerializeField] public float initialValue = 0f;
    [SerializeField] private float emotionStatus;
    const float SERENITY_MAX_VALUE = -100.0f;
    const float RAGE_MAX_VALUE = 100.0f;
    const float FEAR_MAX_VALUE = 100.0f;



    // Start is called before the first frame update
    void Start()
    {
        emotionStatus = initialValue;
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
        }
        if(Input.GetKeyDown("q"))
        {
            IncreaseSerenity(10f);
            Debug.Log("Increase Serenity for 10");
            Debug.Log("current value: " + emotionStatus);
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

    public float getEmotionStatus(){
        return emotionStatus;
    }
}
