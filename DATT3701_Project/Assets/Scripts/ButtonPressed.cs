//using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonPressed : MonoBehaviour
{

    public LayerMask PlayerLayer;
    public GameObject BoardText;
    public GameObject Prompt;
    public GameObject Set;
    private Transform spriteTransform;

    private Vector3 originalPosition;
    private Vector3 targetPosition;

    private bool isPressed;
    public float returnSpeed = 1.0f;

    public float secound = 5;
    private float nextTime = 1;

    private AudioManager audioManager;


    void Start()
    {
        spriteTransform = GetComponent<Transform>();
        originalPosition = spriteTransform.position;
        targetPosition = originalPosition + new Vector3(0.0f, -spriteTransform.localScale.y * 0.5f, 0.0f);

        audioManager = FindObjectOfType<AudioManager>();
        BoardText.GetComponent<TextMeshProUGUI>().color = Color.red;
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

    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & PlayerLayer) != 0)
        {
            isPressed = true;
            transform.position = targetPosition;
        }
    }

    private void OnCollisionExit2D(UnityEngine.Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & PlayerLayer) != 0)
        {
            isPressed = false;
        }
    }


    private void Timing()
    {
        if (secound <= 0)
        {
            if (this.name == "start")
            {
                SceneManager.LoadScene("StartMenu 1");
                GameObject.Find("PlayerManager").GetComponent<PlayerEmotionStatus>().emotionStatus = 0;
                //if (audioManager.checkIsPlaying("SerenityTheme"))
                //{
                //    audioManager.Stop("SerenityTheme");
                //}
            }
            if (this.name == "exit")
            {
                Application.Quit();
            }
            return;
        }

        if (Time.time >= nextTime)
        {
            secound -= 1;
            Debug.Log(secound);
            nextTime = Time.time + 1;
            BoardText.GetComponent<TextMeshProUGUI>().text = "" +secound;

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.name == "credits" && Prompt != null)
        {
            Prompt.SetActive(true);
        }
        if (this.name == "setting" && Set != null)
        {
            Set.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {

        if (this.name == "credits"&& Prompt != null)
        {
            Prompt.SetActive(false);
        }

        if (this.name == "setting" && Set != null)
        {
            Set.SetActive(false);
        }
    }
}
