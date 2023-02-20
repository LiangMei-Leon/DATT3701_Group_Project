using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TutorialWindow : MonoBehaviour
{
    [Header("Header")]
    [SerializeField]
    private Transform headerArea;
    [SerializeField]
    private TextMeshProUGUI titleField;
    
    [Header("Content")]
    [SerializeField]
    private Transform contentArea;
    [SerializeField]
    private Image contentImage;


    [Header("NextButton")]
    [SerializeField]
    private Image NextImage;
    
    [Header("BackButton")]
    [SerializeField]
    private Image BackImage;

    [Header("CloseButton")]
    [SerializeField]
    private Image CloseImage;


    private Action onNext;
    private Action onBack;
    private Action onClose;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
