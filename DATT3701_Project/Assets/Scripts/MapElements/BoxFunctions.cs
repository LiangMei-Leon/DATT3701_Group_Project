using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxFunctions : MonoBehaviour
{
    public float boxLifespan = 2f;
    // public float distance_down = 0.425f;
    // public float distance_right = 0.5f;
    // public float distance_left = 0.5f;
    public float distanceBox_right = 0.4f;
    public float distanceBox_left = 0.4f;
    private GameObject playerManager;
    private PlayerEmotionStatus playerEmotion;
    private float emotionStatus;
    private GameObject normalplayer;
    private CharacterController2D playerData;
    public LayerMask boxMask;
    public LayerMask playerMask;
    public LayerMask Mask;
    private SpriteRenderer boxSprite;

    public bool stackable = false;
    private GameObject object1;

    // public bool playerNearby = false;
    // private bool playerOnRight = false;
    // private bool playerOnLeft = false;
    public bool BoxNearby = false;
    private bool BoxOnRight = false;
    private bool BoxOnLeft = false;
    // private bool floatingL = false;
    // private bool floatingR = false;
    // public bool floating = false;
    private AudioManager audioManager;
    private bool audioPlayed01 = false;

    private ParticleSystem breakVFX1;
	private ParticleSystem breakVFX2;
    private bool used = false;

    private Vector3 savedBoxLoaction;
    private float savedBoxLife = 2;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.FindWithTag("PlayerManager");
        playerEmotion= playerManager.GetComponent<PlayerEmotionStatus>();
        normalplayer = GameObject.FindWithTag("Player");
        playerData = normalplayer.GetComponent<CharacterController2D>();
        boxSprite = gameObject.GetComponent<SpriteRenderer>();
        audioManager = FindObjectOfType<AudioManager>();

        breakVFX1 = this.gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
		breakVFX2 = this.gameObject.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(boxLifespan == 1 && !audioPlayed01)
        {
            audioPlayed01 = true;
            boxSprite.sprite = Resources.Load<Sprite>("box2");
            audioManager.Play("BoxCracked01");
            breakVFX1.Play();
        }
        if(boxLifespan <= 0 && !used)
        {
            used = true;
            boxSprite.color = new Color(1,1,1,0);
            foreach (Collider2D c in this.GetComponents<Collider2D>())
            {
                if(c.enabled == true)
                    c.enabled = false;
            }
            breakVFX2.Play();
            audioManager.Play("BoxCracked02");
            //Destroy(gameObject, 0.5f);
        }
        emotionStatus = playerEmotion.getEmotionStatus();
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hitBoxRight = Physics2D.Raycast(transform.position, Vector2.right, distanceBox_right, boxMask);
        if (hitBoxRight.collider != null){
            BoxOnRight = true;
        }else{
            BoxOnRight = false;
        }
        RaycastHit2D hitBoxLeft = Physics2D.Raycast(transform.position, Vector2.left, distanceBox_left, boxMask);
        if(hitBoxLeft.collider != null){
            BoxOnLeft = true;
        }else{
            BoxOnLeft = false;
        }
        if(BoxOnRight || BoxOnLeft){
            BoxNearby = true;
        }else{
            BoxNearby = false;
        }
        if(playerData.IsJumping){
            this.GetComponent<Rigidbody2D>().mass = 2f;
        }else{
            this.GetComponent<Rigidbody2D>().mass = 1f;
        }
        if(BoxNearby || (emotionStatus > 0)){
            this.GetComponent<Rigidbody2D>().mass = 25f;
        }
        // RaycastHit2D hitPlayerRight = Physics2D.Raycast((Vector2)transform.position + Vector2.down * 0.3f, Vector2.right, distance_right, playerMask);
        // if (hitPlayerRight.collider != null){
        //     playerOnRight = true;
        // }else{
        //     playerOnRight = false;
        // }
        // RaycastHit2D hitPlayerLeft = Physics2D.Raycast((Vector2)transform.position + Vector2.down * 0.3f, Vector2.left, distance_left, playerMask);
        // if (hitPlayerLeft.collider != null){
        //     playerOnLeft = true;
        // }else{
        //     playerOnLeft = false;
        // }
        // if(playerOnLeft || playerOnRight){
        //     playerNearby = true;
        // }else{
        //     playerNearby = false;
        // }
        // RaycastHit2D hitCheckL = Physics2D.Raycast((Vector2)transform.position + Vector2.left * 0.38f, Vector2.down, distance_down, Mask);
        // if (hitCheckL.collider == null){
        //     floatingL = true;
        // }else{
        //     floatingL = false;
        // }RaycastHit2D hitCheckR = Physics2D.Raycast((Vector2)transform.position + Vector2.right * 0.38f, Vector2.down, distance_down, Mask);
        // if (hitCheckR.collider == null){
        //     floatingR = true;
        // }else{
        //     floatingR = false;
        // }
        // if(floatingL && floatingR){
        //     floating = true;
        // }else{
        //     floating = false;
        // }
        // RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distance_down, boxMask);
        // if (hit.collider != null){
        //     object1 = hit.collider.gameObject;
        //     BoxFunctions objectData = object1.GetComponent<BoxFunctions>();
        //     if(!playerNearby)
        //     {
        //         object1.GetComponent<FixedJoint2D>().enabled = true;
        //         object1.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
        //     }else{
        //         object1.GetComponent<FixedJoint2D>().enabled = false;
        //     }
        //     if(objectData.floating){
        //         object1.GetComponent<FixedJoint2D>().enabled = false;
        //     }
        // }
    }

    void FixedUpdate()
     {
        Vector2 currentVelocity = GetComponent<Rigidbody2D>().velocity;
  
        if (currentVelocity.y <= 0f) 
            return;
          
        currentVelocity.y = 0f;
          
        GetComponent<Rigidbody2D>().velocity = currentVelocity;
    }

    public void Smash(){
        boxLifespan--;
    }
    
    public void UpdateBoxStatus()
    {
        savedBoxLoaction = this.transform.position;
        savedBoxLife = boxLifespan;
    }

    public void RewindBox()
    {
        if(this.boxLifespan == 0 && savedBoxLife == 1){
            this.boxLifespan = savedBoxLife;
            audioPlayed01 = true;
            used = false;
            boxSprite.sprite = Resources.Load<Sprite>("box2");
            boxSprite.color =  Color.white;
            foreach (Collider2D c in this.GetComponents<Collider2D>())
            {
                if(c.enabled == false)
                    c.enabled = true;
            }
            this.transform.position = savedBoxLoaction;
        }else if(this.boxLifespan == 0 && savedBoxLife == 2){
            this.boxLifespan = savedBoxLife;
            audioPlayed01 = false;
            used = false;
            boxSprite.sprite = Resources.Load<Sprite>("box");
            boxSprite.color =  Color.white;
            foreach (Collider2D c in this.GetComponents<Collider2D>())
            {
                if(c.enabled == false)
                    c.enabled = true;
            }
            this.transform.position = savedBoxLoaction;
        }else if(this.boxLifespan == 1 && savedBoxLife == 2){
            this.boxLifespan = savedBoxLife;
            audioPlayed01 = false;
            boxSprite.sprite = Resources.Load<Sprite>("box");
            boxSprite.color =  Color.white;
            this.transform.position = savedBoxLoaction;
        }else if(this.boxLifespan == savedBoxLife){
            this.transform.position = savedBoxLoaction;
        }

    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(stackable){
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
    void OnDrawGizmos()
    {
        // Gizmos.color = Color.red;
        // Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.down * distance_down);
        // Gizmos.DrawLine((Vector2)transform.position + Vector2.down * 0.3f, (Vector2)transform.position + Vector2.left * distance_left + Vector2.down * 0.3f);
        // Gizmos.DrawLine((Vector2)transform.position + Vector2.down * 0.3f, (Vector2)transform.position + Vector2.right * distance_right + Vector2.down * 0.3f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.left * distanceBox_left);
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * distanceBox_right);
        // Gizmos.DrawLine((Vector2)transform.position + Vector2.left * 0.38f, (Vector2)transform.position + Vector2.left * 0.38f + Vector2.down * distance_down);
        // Gizmos.DrawLine((Vector2)transform.position + Vector2.right * 0.38f, (Vector2)transform.position + Vector2.right * 0.38f + Vector2.down * distance_down);
    }
}
