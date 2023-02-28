using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MeterUI : MonoBehaviour
{
    public GameObject leftmax;
    public GameObject rightmax;
    [Tooltip("put the absolute value of my x position in the inspector here")]
    public float myXposition;
    [SerializeField] 
    private Image imageNeedle;
    [SerializeField] 
    private Slider slider;



   
   //variable for controlling meter needle speed
   public float currentEmoStatus;
   public float targetEmoStatus;
   public float needleSpeed = 100f;




    // Update is called once per frame
    void Update()
    {
        
        if(targetEmoStatus != currentEmoStatus){
            UpdateEmo();
        }
    }


    //Method for calculate Meter distacne
   

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
            currentEmoStatus = Mathf.Clamp(currentEmoStatus, -100.0f, targetEmoStatus);
        }else if(targetEmoStatus < currentEmoStatus)
        {
            currentEmoStatus -= Time.deltaTime * needleSpeed;
            currentEmoStatus = Mathf.Clamp(currentEmoStatus, targetEmoStatus,100.0f); 
        }

        SetNeedle();
    
    }

    void SetNeedle(){
        imageNeedle.transform.localPosition = new Vector3((((currentEmoStatus / 200.0f * (rightmax.transform.localPosition.x - leftmax.transform.localPosition.x)) - myXposition) * 1.0f), gameObject.transform.localPosition.y, 0);
                                                            // currentEmoStatus / 200.0f *   总长  -   初始X位置                                                 
    }


}
