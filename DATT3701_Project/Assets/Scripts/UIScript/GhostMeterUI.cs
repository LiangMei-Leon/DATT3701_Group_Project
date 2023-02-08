using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostMeterUI : MonoBehaviour
{
  [SerializeField] Slider GhostMeter;
  [SerializeField] Slider testSlider;

  void Start(){
    GhostMeter = GetComponent<Slider>();
  }

  public void setFear(float fearValue){
    fearValue = Mathf.Clamp(fearValue, 0.0f, 100.0f);
    GhostMeter.value = fearValue;
    }
    
  

    public void setFearBySlider(){
         GhostMeter.value = testSlider.value;
    }


}
