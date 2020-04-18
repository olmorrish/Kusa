using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject playerReference;

    public float moveForce;
    public float currentMaxSpeed;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
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

        //find the nearest of the locations, then move towards it
        Vector2 closest = Closest(possibleLocations);
        Vector2 moveVector = new Vector2(closest.x - transform.position.x, closest.y - transform.position.y);
        rb.AddForce(moveVector.normalized * moveForce, ForceMode2D.Force);

        //restrict speed
        RestrictVelocity(currentMaxSpeed);
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
