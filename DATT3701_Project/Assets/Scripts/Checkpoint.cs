using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameObject playerManager;
    private ProgressRewind playerProgress;
    private bool saved = false;
    private SpriteRenderer highlight;
    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.FindWithTag("PlayerManager");
        playerProgress = playerManager.GetComponent<ProgressRewind>();
        highlight = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player") && !saved){
            playerProgress.save();
            saved = true;
            highlight.color = Color.white;
        }
    }
}
