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

    void Update(){
    }

    IEnumerator LoadScene(){
        transitionAnim.SetTrigger("end");
        text.SetActive(true);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(sceneName);
    }


    private void OnTriggerEnter2D(Collider2D other){
        print("Trigger Entered");
        if(other.tag == "Player"){
            StartCoroutine(LoadScene());
        }
    }


}
