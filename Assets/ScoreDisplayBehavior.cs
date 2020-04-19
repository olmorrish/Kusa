using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDisplayBehavior : MonoBehaviour
{
    public int pointsPerPowerLevel;
    public int points;

    private Animator anim;
    private Transform tr;

    void Start() {
        anim = GetComponent<Animator>();
        tr = GetComponent<Transform>();
    }

    private void FixedUpdate() {
        //set points to wherever that number is held
        anim.SetInteger("score", points);
        tr.localScale = new Vector2(1, 1) * (3 * (points % pointsPerPowerLevel) + pointsPerPowerLevel) / pointsPerPowerLevel;
    }
}
