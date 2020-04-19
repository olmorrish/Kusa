using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour {

    //Player State and Animation
    public enum PlayerState {
        Idle, 
        Growing,
        Shrinking
    }
    public PlayerState currentState;
    private Animator anim;

    //Movement Speed
    public float initialAnimatorSpeed;
    public float powerupAnimSpeedIncrease;
    private Vector3 target;
    public float moveDistance; //the distance the grass will move

    //Prefabs
    public GameObject grassPatchPrefab;

    //Collision Checking
    public LayerMask whatIsSolid;
    public float hitCheckSize;

    //Scoring and Progress
    public int powerLevel;


    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
        anim.speed = initialAnimatorSpeed;
        powerLevel = 0;
    }

    // Update is called once per frame
    void Update() {

        UpdateAnimator();
        //UpdatePowerLevel(); //TODO call this on beat
        
        switch (currentState) {

            // STATE: IDLE
            case PlayerState.Idle:
                //wait for input
                //TODO

                float jitterX = Random.Range(-0.2f, 0.2f);
                float jitterY = Random.Range(-0.2f, 0.2f);

                //on input, if teleport location is valid, set state to SHRINKING
                if (Input.GetButton("left")) {
                    target = new Vector3(transform.position.x - moveDistance * 1.5f + jitterX, transform.position.y + jitterY);
                    currentState = PlayerState.Shrinking;   //this animation calls teleport to the target on its last frame
                }
                else if (Input.GetButton("right")) {
                    target = new Vector3(transform.position.x + moveDistance * 1.5f + jitterX, transform.position.y + jitterY);
                    currentState = PlayerState.Shrinking;   //this animation calls teleport to the target on its last frame
                }
                else if (Input.GetButton("up")) {
                    target = new Vector3(transform.position.x + jitterX, transform.position.y + moveDistance + jitterY);
                    currentState = PlayerState.Shrinking;   //this animation calls teleport to the target on its last frame
                }
                else if (Input.GetButton("down")) {
                    target = new Vector3(transform.position.x + jitterX, transform.position.y - moveDistance + jitterY);
                    currentState = PlayerState.Shrinking;   //this animation calls teleport to the target on its last frame
                }

                break;

            // STATE: GROWING
            case PlayerState.Growing:
                break;


            // STATE: SHRINKING
            case PlayerState.Shrinking:
                break;
        }


    }


    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("PowerUp")) {
            anim.speed += powerupAnimSpeedIncrease;
            GameObject.Destroy(collision.gameObject);
        }
    }



    /* Teleport To
     * Called as an animation event when the grass in despawning
     * This also instantiates a patch of grass in the position before leaving
     */
    public bool Teleport() {

        if (!TargetBlocked()) {
            SpawnGrassPatch();
            currentState = PlayerState.Growing;
            transform.position = target;
            return true;
        }
        else {
            currentState = PlayerState.Idle;
            return false;
        }

    }


    public bool TargetBlocked(){

        Collider2D[] collisions = Physics2D.OverlapCircleAll(target, hitCheckSize, whatIsSolid);
        if (collisions.Length > 0)
            return true;
        else
            return false;
    }

    /* Spawn Grass Patch
     * Instantiates a grass patch prefab at the player's current position.
     */
    private void SpawnGrassPatch() {
        GameObject newPatch = Instantiate(grassPatchPrefab, transform.position, rotation: Quaternion.identity);

    }

    /* Force State
     * This method is EXCLUSIVELY called by animation events, and is used to force a state change once an animation is concluded, 
     *  used in the case that an animation must be played in full. 
     */
    public void ForceState(PlayerState newState) {
        currentState = newState;
    }


    /*
     * 
     * 
     */
    private void UpdateAnimator() {

        //TODO
        switch (currentState) {
            case PlayerState.Idle:
                anim.SetBool("idle", true);
                anim.SetBool("growing", false);
                anim.SetBool("shrinking", false);
                break;
            case PlayerState.Growing:
                anim.SetBool("idle", false);
                anim.SetBool("growing", true);
                anim.SetBool("shrinking", false);
                break;
            case PlayerState.Shrinking:
                anim.SetBool("idle", false);
                anim.SetBool("growing", false);
                anim.SetBool("shrinking", true);
                break;
        }
    }


    /* Update Power Level
     * Increases the power level by the number of patches of grass currently onscreen.
     */
    public void UpdatePowerLevel() {
        GameObject[] patches = GameObject.FindGameObjectsWithTag("Patch");
        powerLevel += patches.Length;   //this counts the player as a patch as well btw
    }
}
