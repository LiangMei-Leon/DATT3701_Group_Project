using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public Animator transitionAnim;
    public string sceneName;

    public int sceneBuildIndex;
    public GameObject text;
    public bool isFinalLevel = false;
    public GameObject congratsPanel;
    public GameObject pauseShade;

    private AudioManager audioManager;

    void Start(){
        audioManager = FindObjectOfType<AudioManager>();
    }

    IEnumerator LoadScene(){
        audioManager.Play("PickFlower");
        transitionAnim.SetTrigger("end");
        text.SetActive(true);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(sceneName);
    }


    private void OnTriggerEnter2D(Collider2D other){
        print("Trigger Entered");
        if(other.tag == "Player" & !isFinalLevel){
            StartCoroutine(LoadScene());
        }else if(other.tag == "Player" & isFinalLevel){
            pauseShade.SetActive(true);
            congratsPanel.SetActive(true);
        }
    }


}
