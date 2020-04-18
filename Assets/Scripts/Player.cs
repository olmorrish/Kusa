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
    int visualEnergyLevel;


    public GameObject grassPatchPrefab;

    private Vector3 target;
    public float speed = 10f;
    public float moveDistance; //the distance the grass will move

    public LayerMask whatIsWall;
    public float hitCheckSize;

    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {

        
        switch (currentState) {

            // STATE: IDLE
            case PlayerState.Idle:
                //wait for input
                float verticalIn = Input.GetAxis("Vertical");
                float horizontalIn = Input.GetAxis("Horizontal");

                //TODO
                //on input, if teleport location is valid, set state to SHRINKING
                break;

            // STATE: GROWING
            case PlayerState.Growing:
                break;



            // STATE: SHRINKING
            case PlayerState.Shrinking:
                break;
        }


    }


    /*
     * 
     */
     public bool TeleportTo(Vector2 target) {
        transform.position = target;
        return true; //TODO add a fail condition
    }


    public bool CollisionInDirection(Vector2 target) {

        Collider2D[] collisions = Physics2D.OverlapCircleAll(target, hitCheckSize, whatIsWall);
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

        anim.SetInteger("energyLevel", visualEnergyLevel);

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
}
