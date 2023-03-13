using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public Animator transitionAnim;
    public string sceneName;

    public int sceneBuildIndex;

    void Update(){
        if(Input.GetKeyDown(KeyCode.T)){
            StartCoroutine(LoadScene());
        }
    }

    IEnumerator LoadScene(){
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }


    private void OnTriggerEnter2D(Collider2D other){
        print("Trigger Entered");
        if(other.tag == "Player"){
            StartCoroutine(LoadScene());
        }
    }


}
