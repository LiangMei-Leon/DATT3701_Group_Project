//using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ButtonPressed : MonoBehaviour
{
    public GameObject BoardText;
    private Transform spriteTransform;

    private Vector3 originalPosition;
    private Vector3 targetPosition;

    [Tooltip("Setting, Credit, Exit, Level**")]
    public string areaMode;
    public GameObject settingPanel;
    public GameObject pauseShade;

    private bool isPressed = false;
    private float returnSpeed = 1.0f;

    public float secound = 3;
    private float nextTime = 1;
    private bool activated = false;
    private AudioManager audioManager;


    private GameObject transitionAnim;
    private Animator _traAnimator;


    void Start()
    {
        spriteTransform = GetComponent<Transform>();
        originalPosition = spriteTransform.position;
        targetPosition = originalPosition + new Vector3(0.0f, -spriteTransform.localScale.y * 0.5f, 0.0f);

        audioManager = FindObjectOfType<AudioManager>();
        transitionAnim = GameObject.FindWithTag("Transition");
        _traAnimator = transitionAnim .GetComponent<Animator>();
    }

    void Update()
    {
        if (!isPressed)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, returnSpeed * Time.deltaTime);
        }
        else
        {
            Timing();
        }
    }

    private void Timing()
    {
        if (secound <= 0 && !activated)
        {
            if (areaMode == "Setting")
            {
                activated =  true;
                audioManager.Play("PanelToggle");
                settingPanel.SetActive(true);
                pauseShade.SetActive(true);
                Debug.Log("open setting panel");
            }
            if (areaMode == "Credit")
            {
                activated =  true;
                Debug.Log("open credit panel");
            }
            if (areaMode == "Exit")
            {
                
                Application.Quit();
            }
            if (areaMode == "LEVEL-1")
            {
                activated =  true;
                StartCoroutine(PlayAnim("LEVEL-1"));
            }
            if (areaMode == "LEVEL-2")
            {
                 activated =  true;
                StartCoroutine(PlayAnim("LEVEL-2"));
            }
            if (areaMode == "LEVEL-3")
            {
                 activated =  true;
                StartCoroutine(PlayAnim("LEVEL-3"));
            }
            if (areaMode == "LEVEL-4")
            {
                 activated =  true;
                StartCoroutine(PlayAnim("LEVEL-4"));
            }
            if (areaMode == "LEVEL-5")
            {
                 activated =  true;
                StartCoroutine(PlayAnim("LEVEL-5"));
            }
            if (areaMode == "LEVEL-6")
            {
                 activated =  true;
                StartCoroutine(PlayAnim("LEVEL-6"));
            }
            if (areaMode == "LEVEL-7")
            {
                 activated =  true;
                StartCoroutine(PlayAnim("LEVEL-7"));
            }
            if (areaMode == "LEVEL-8")
            {
                 activated =  true;
                StartCoroutine(PlayAnim("LEVEL-8"));
            }
            if (areaMode == "LEVEL-9")
            {
                 activated =  true;
                StartCoroutine(PlayAnim("LEVEL-9"));
            }
            return;
        }else if(Time.time >= nextTime && !activated)
        {
            secound -= 1;
            audioManager.Play("BigButton");
            nextTime = Time.time + 1;
            BoardText.GetComponent<TextMeshPro>().text = "" + secound;

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isPressed)
        {
            isPressed = true;
            audioManager.Play("ClickButton");
            transform.position = targetPosition;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isPressed)
        {
            isPressed = false;
            activated = false;
            secound = 4;
            nextTime = 1;
            if (areaMode == "Setting")
            {
                BoardText.GetComponent<TextMeshPro>().text = "Setting";
            }
            if (areaMode == "Credit")
            {
                BoardText.GetComponent<TextMeshPro>().text = "Credit";
            }
            if (areaMode == "Exit")
            {
                BoardText.GetComponent<TextMeshPro>().text = "Exit";
            }
            if (areaMode == "LEVEL-1")
            {
                BoardText.GetComponent<TextMeshPro>().text = "LEVEL 1";
            }
            if (areaMode == "LEVEL-2")
            {
                BoardText.GetComponent<TextMeshPro>().text = "LEVEL 2";
            }
            if (areaMode == "LEVEL-3")
            {
                BoardText.GetComponent<TextMeshPro>().text = "LEVEL 3";
            }
            if (areaMode == "LEVEL-4")
            {
                BoardText.GetComponent<TextMeshPro>().text = "LEVEL 4";
            }
            if (areaMode == "LEVEL-5")
            {
                BoardText.GetComponent<TextMeshPro>().text = "LEVEL 5";
            }
            if (areaMode == "LEVEL-6")
            {
                BoardText.GetComponent<TextMeshPro>().text = "LEVEL 6";
            }
            if (areaMode == "LEVEL-7")
            {
                BoardText.GetComponent<TextMeshPro>().text = "LEVEL 7";
            }
            if (areaMode == "LEVEL-8")
            {
                BoardText.GetComponent<TextMeshPro>().text = "LEVEL 8";
            }
            if (areaMode == "LEVEL-9")
            {
                BoardText.GetComponent<TextMeshPro>().text = "LEVEL 9";
            }
        }
    }


    IEnumerator PlayAnim(string levelName){
        //Debug.Log("adasdsad");
        _traAnimator.SetTrigger("end");
        //text.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        //Debug.Log("adasdsassssss22222s222d");
        SceneManager.LoadScene(levelName);
        
    }






}
