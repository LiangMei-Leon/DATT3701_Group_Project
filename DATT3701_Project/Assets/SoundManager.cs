using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource source;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            slider.value = (float)((float)slider.value + 0.1);
            source.volume = slider.value;
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            slider.value = (float)((float)slider.value - 0.1);
            source.volume = slider.value;
        }
    }
}
