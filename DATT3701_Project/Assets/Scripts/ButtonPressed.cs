//using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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


    void Start()
    {
        spriteTransform = GetComponent<Transform>();
        originalPosition = spriteTransform.position;
        targetPosition = originalPosition + new Vector3(0.0f, -spriteTransform.localScale.y * 0.5f, 0.0f);

        audioManager = FindObjectOfType<AudioManager>();
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
                activated =  true;
                Application.Quit();
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
        }
    }
}
