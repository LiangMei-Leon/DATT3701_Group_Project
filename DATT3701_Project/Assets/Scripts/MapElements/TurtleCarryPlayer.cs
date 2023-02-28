using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleCarryPlayer : MonoBehaviour
{
    private GameObject object1;
    private GameObject playerManager;
    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.FindWithTag("PlayerManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
        if(other.gameObject.CompareTag("Boxes"))
        {
            other.transform.SetParent(transform);
            object1 = other.gameObject;
            BoxFunctions objectData = object1.GetComponent<BoxFunctions>();
            objectData.stackable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(playerManager.transform);
        }
        if(other.gameObject.CompareTag("Boxes"))
        {
            other.transform.SetParent(null);
            object1 = other.gameObject;
            BoxFunctions objectData = object1.GetComponent<BoxFunctions>();
            objectData.stackable = false;
        }
    }
}
