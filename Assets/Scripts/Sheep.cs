using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIMode {
    Closest,
    Farthest,
    Random, 
    PursuingSingleTarget,
    Player
}

public class Sheep : MonoBehaviour
{
    //AI Modes
    public AIMode mode;
    private AIMode previousMode; //used to save what the original mode was when a sheep pursues a single target
    public Vector2 singleTarget;

    //Component and Object References
    private Rigidbody2D rb;
    private Transform tr;
    public GameObject playerReference;

    //Poses
    private Animator topSpriteAnim;
    public int currentPoseNum;
    public int totalNumPoses;

    //Manure Spawns
    public GameObject manurePrefab;
    public float manureSpawnInterval;
    private float nextManureSpawnTime;

    //Movement
    public float moveForce;
    public float currentMaxSpeed;
    private float nextSpeedUpTime;
    public float speedUpInterval;
    public float speedUpBy;

    //Sound
    public GameObject soundEffectParentObject;
    private FXController fxController;
    

    
    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();

        playerReference = GameObject.Find("PlayerGrass");

        nextManureSpawnTime = Time.time + Random.Range(0, manureSpawnInterval);
        nextSpeedUpTime = Time.time + speedUpInterval; 

        soundEffectParentObject = GameObject.Find("SoundEffects");
        fxController = soundEffectParentObject.GetComponent<FXController>();

        topSpriteAnim = gameObject.GetComponentInChildren<Animator>();
        currentPoseNum = -1;
        NewPose();
    }

    // Update is called once per frame
    void FixedUpdate() {
        maintainSize();

        GameObject[] possibleTargets = GameObject.FindGameObjectsWithTag("Patch");
        int numPatches = possibleTargets.Length;

        //gather the locations of all possible targets (all patches, and player)
        Vector2[] possibleLocations = new Vector2[numPatches + 1];
        for (int i=0; i<numPatches; i++) {
            possibleLocations[i] = possibleTargets[i].transform.position;
        }
        possibleLocations[numPatches] = playerReference.transform.position;


        //decide which direction to go based on the AI Mode
        Vector2 moveVector = Vector2.zero;
        switch (mode) {

            //AI MODE CLOSEST: find the nearest of the locations, then move towards it
            case AIMode.Closest:
                Vector2 closest = Closest(possibleLocations);
                moveVector = new Vector2(closest.x - transform.position.x, closest.y - transform.position.y);
                rb.AddForce(moveVector.normalized * moveForce, ForceMode2D.Force);
                break;

            //AI MODE FARTHEST: find the farthest of the locations, then move towards it (saves the target until reached)
            case AIMode.Farthest:
                Vector2 farthest = Farthest(possibleLocations);
                singleTarget = farthest;
                moveVector = new Vector2(farthest.x - transform.position.x, farthest.y - transform.position.y);
                previousMode = AIMode.Farthest;
                mode = AIMode.PursuingSingleTarget;
                break;

            //AI MODE RANDOM: pick a patch at random, then move towards it (saves the target until reached)
            case AIMode.Random:
                Vector2 rand = possibleLocations[Random.Range(0, possibleLocations.Length - 1)]; ;
                singleTarget = rand;
                moveVector = new Vector2(rand.x - transform.position.x, rand.y - transform.position.y);
                previousMode = AIMode.Random;
                mode = AIMode.PursuingSingleTarget;
                break;

            //AI MODE PURSUING TARGET: moves towards a saved point until it is reached, then returns to previous AI mode
            case AIMode.PursuingSingleTarget:
                moveVector = new Vector2(singleTarget.x - transform.position.x, singleTarget.y - transform.position.y);
                if (Vector2.Distance(transform.position, singleTarget) < 0.75f) {    //if we get close enough to the target, look for a new one
                    mode = previousMode;
                }
                break;

            //AI MODE PLAYER: always moves directly towards the player
            case AIMode.Player:
                moveVector = new Vector2(playerReference.transform.position.x - transform.position.x, 
                                         playerReference.transform.position.y - transform.position.y);
                break;
        }

        //now that the decision of where to go is handled, apply the force
        rb.AddForce(moveVector.normalized * moveForce, ForceMode2D.Force);

        //restrict speed
        RestrictVelocity(currentMaxSpeed);

        //add random pulse if stuck, and if a couple second have passed in the scene
        if (rb.velocity.magnitude < 0.01f && Time.time > 2f) {
            Vector2[] randomVectors = { Vector2.up * 4, Vector2.down * 4, Vector2.left * 4, Vector2.right * 4 };
            Vector2 randomDir = randomVectors[Random.Range(0, 4)];
            rb.AddForce(randomDir, ForceMode2D.Impulse);
        }

        //Check if it's time to spawn a manure powerup
        if (Time.time > nextManureSpawnTime) {
            Instantiate(manurePrefab, transform.position, rotation: Quaternion.identity);
            nextManureSpawnTime = Time.time + manureSpawnInterval;
            fxController.PlayEffect("Fart");
        }

        //check if it's time to speed up
        if (Time.time > nextSpeedUpTime) {
            currentMaxSpeed += speedUpBy;
            nextSpeedUpTime = Time.time + speedUpInterval;
        }
    }

    /* Closest
     * Given an array of Vector2s, determines which is closest to this object.
     */
    private Vector2 Closest(Vector2[] possibleLocations) {

        float closestSoFar = float.MaxValue;
        int indexOfClosest = -1;

        for (int i=0; i<possibleLocations.Length; i++) {
            float dist = Vector2.Distance(transform.position, possibleLocations[i]);
            if (dist < closestSoFar) {
                closestSoFar = dist;
                indexOfClosest = i;
            }
        }

        //Debug.Log(possibleLocations[indexOfClosest]);
        return possibleLocations[indexOfClosest];
    }


    /* Farthest
     * Given an array of Vector2s, determines which is farthest from this object.
     */
    private Vector2 Farthest(Vector2[] possibleLocations) {

        float farthestSoFar = float.MinValue;
        int indexOfFarthest = -1;

        for (int i = 0; i < possibleLocations.Length; i++) {
            float dist = Vector2.Distance(transform.position, possibleLocations[i]);
            if (dist > farthestSoFar) {
                farthestSoFar = dist;
                indexOfFarthest = i;
            }
        }

        //Debug.Log(possibleLocations[indexOfFarthest]);
        return possibleLocations[indexOfFarthest];
    }



    /* Restrict Velocity
     * Limits the x and y velocities if they exceed a given amount
     */
    private void RestrictVelocity(float max) {
        
        if(rb.velocity.x > max) {
            rb.velocity = new Vector2(max, rb.velocity.y);
        }
        else if (rb.velocity.x < -max) {
            rb.velocity = new Vector2(-max, rb.velocity.y);
        }

        if (rb.velocity.y > max) {
            rb.velocity = new Vector2(rb.velocity.x, max);
        }
        else if (rb.velocity.y < -max) {
            rb.velocity = new Vector2(rb.velocity.x, -max);
        }

    }

    /* New Pose
     * Changes the top half of the sheep to a random new pose
     * This function is called for all sheep on certain beats
     */
    public void NewPose() {
        int newPoseNum = Random.Range(1,totalNumPoses + 1);

        while (newPoseNum == currentPoseNum) {                  //generate a random until we're guarenteed a new pose
            newPoseNum = Random.Range(1, totalNumPoses + 1);
        }

        topSpriteAnim.SetInteger("poseNum", newPoseNum);
        currentPoseNum = newPoseNum;
    }

    /// <summary>
    /// returns size to normal after on Beat expansion
    /// </summary>
    private void maintainSize()
    {
        if(tr.localScale.x > 1)
        {
            tr.localScale -= new Vector3(0.02f, 0.02f, 0.02f);
        }
    }
}
