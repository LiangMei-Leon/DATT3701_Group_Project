using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDetection : MonoBehaviour
{
    public GameObject myCam;
    public GameObject myBg;
    private Vector3 velocity = Vector3.zero;
    public GameObject cameraDestination1;
    public GameObject cameraDestination2;
    private bool exitLeft = false;
    private bool exitRight = false;

    [Tooltip("Play, Setting, Credit, Exit, Level**")]
    public string areaMode;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(exitRight){
            //player move from left to right panel
            myCam.transform.position = Vector3.SmoothDamp(myCam.transform.position, cameraDestination1.transform.position, ref velocity, 1f);
            myBg.transform.position = Vector3.SmoothDamp(myBg.transform.position, cameraDestination1.transform.position, ref velocity, 1f);
        }else if(exitLeft){
            //player move from right to left panel
            myCam.transform.position = Vector3.SmoothDamp(myCam.transform.position, cameraDestination2.transform.position, ref velocity, 1f);
            myBg.transform.position = Vector3.SmoothDamp(myBg.transform.position, cameraDestination2.transform.position, ref velocity, 1f);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        
    }

    void OnTriggerStay2D(Collider2D col)
    {
        
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(areaMode == "Play" && col.gameObject.CompareTag("Player")){
            if(this.transform.position.x > col.gameObject.transform.position.x){
                exitLeft = true;
                exitRight = false;
            }else{
                exitRight = true;
                exitLeft = false;
            }
        }
    }
}
