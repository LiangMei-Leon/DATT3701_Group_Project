using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MeterUI : MonoBehaviour
{
    [SerializeField] 
    private Image imageNeedle;
    [SerializeField] 
    private Slider slider;

   
   //variable for controlling meter needle speed
   public float currentEmoStatus = 0;
   public float targetEmoStatus = 0;
   public float needleSpeed = 100f;
   
    // Update is called once per frame
    void Update()
    {
        if(targetEmoStatus != currentEmoStatus){
            UpdateEmo();
        }
    }

    // Method for testing UI with the Slider
    public void SetEmoFromSlider(){
        targetEmoStatus = slider.value;
    }

    //Method for UI calling from another script
    public void setEmo(float amt){
        targetEmoStatus = amt;
    }

    void UpdateEmo(){
        if(targetEmoStatus > currentEmoStatus){
            currentEmoStatus += Time.deltaTime * needleSpeed;
            currentEmoStatus = Mathf.Clamp(currentEmoStatus, 0.0f, targetEmoStatus);
        }else if(targetEmoStatus < currentEmoStatus)
        {
            currentEmoStatus -= Time.deltaTime * needleSpeed;
            currentEmoStatus = Mathf.Clamp(currentEmoStatus, targetEmoStatus,200.0f); 
        }

        SetNeedle();
    
    }

    void SetNeedle(){
        imageNeedle.transform.localEulerAngles = new Vector3(0, 0, (currentEmoStatus / 200.0f * 94.0f - 47.0f)* -1.0f);
    }


}
