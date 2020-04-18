using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIMode {
    Closest,
    Farthest,
    PursuingSingleTarget
}

public class Sheep : MonoBehaviour
{
    public AIMode mode;
    public Vector2 singleTarget;

    private Rigidbody2D rb;
    public GameObject playerReference;

    public GameObject manurePrefab;
    public float manureSpawnInterval;
    private float nextManureSpawnTime;

    public float moveForce;
    public float currentMaxSpeed;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        nextManureSpawnTime = Time.time + Random.RandomRange(0, manureSpawnInterval);
    }

    // Update is called once per frame
    void Update() {
        GameObject[] possibleTargets = GameObject.FindGameObjectsWithTag("Patch");
        int numPatches = possibleTargets.Length;

        //gather the locations of all possible targets (all patches, and player)
        Vector2[] possibleLocations = new Vector2[numPatches + 1];
        for (int i=0; i<numPatches; i++) {
            possibleLocations[i] = possibleTargets[i].transform.position;
        }
        possibleLocations[numPatches] = playerReference.transform.position;



        Vector2 moveVector = Vector2.zero;
        switch (mode) {
            case AIMode.Closest:
                //find the nearest of the locations, then move towards it
                Vector2 closest = Closest(possibleLocations);
                moveVector = new Vector2(closest.x - transform.position.x, closest.y - transform.position.y);
                rb.AddForce(moveVector.normalized * moveForce, ForceMode2D.Force);
                break;
            case AIMode.Farthest:
                //find the nearest of the locations, then move towards it
                Vector2 farthest = Farthest(possibleLocations);
                singleTarget = farthest;
                moveVector = new Vector2(farthest.x - transform.position.x, farthest.y - transform.position.y);
                mode = AIMode.PursuingSingleTarget;
                break;
            case AIMode.PursuingSingleTarget:
                moveVector = new Vector2(singleTarget.x - transform.position.x, singleTarget.y - transform.position.y);
                if (Vector2.Distance(transform.position, singleTarget) < 0.5f) {    //if we get close enough to the target, look for a new one
                    mode = AIMode.Farthest;
                }
                break;
        }

        rb.AddForce(moveVector.normalized * moveForce, ForceMode2D.Force);





        //restrict speed
        RestrictVelocity(currentMaxSpeed);

        //add upwards pulse if stuck
        if (rb.velocity.magnitude < 0.01f) {
            rb.AddForce(Vector2.up + Vector2.left, ForceMode2D.Impulse);
        }

        //Check if it's time to spawn a manure powerup
        if (Time.time > nextManureSpawnTime) {
            Instantiate(manurePrefab, transform.position, rotation: Quaternion.identity);
            nextManureSpawnTime = Time.time + manureSpawnInterval;
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

        Debug.Log(possibleLocations[indexOfClosest]);
        return possibleLocations[indexOfClosest];
    }


    /* Farthest
     * Given an array of Vector2s, determines which is closest to this object.
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

        Debug.Log(possibleLocations[indexOfFarthest]);
        return possibleLocations[indexOfFarthest];
    }



    /*
     * 
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
}
