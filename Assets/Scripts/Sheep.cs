using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject playerReference;

    Transform hitboxPos;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        hitboxPos = GetComponent<CircleCollider2D>().transform;
    }

    // Update is called once per frame
    void Update() {

        GameObject[] possibleTargets = GameObject.FindGameObjectsWithTag("Patch");
        Vector2[] targetLocations = null; 


    }
}
