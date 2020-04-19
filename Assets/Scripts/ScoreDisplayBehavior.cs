using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDisplayBehavior : MonoBehaviour
{
    public int pointsPerPowerLevel;
    /// <summary>
    /// percentage by which the scale yearns for the ideal ~5 works
    /// </summary>
    public float growthPercentile;
    /// <summary>
    /// Smallest scale achievable
    /// </summary>
    public float baseSize;
    /// <summary>
    /// The factor by which the kanji increases in size
    /// </summary>
    public float expansionFactor;

    private Animator anim;
    private Transform tr;
    private int points;
    private Vector2 scaleTarget;
    /// <summary>
    /// reference to the player script
    /// </summary>
    private Player pScript;

    void Start() {
        anim = GetComponent<Animator>();
        tr = GetComponent<Transform>();
        pScript = GameObject.Find("PlayerGrass").GetComponent<Player>();
    }

    private void FixedUpdate() {
        points = pScript.powerLevel;
        anim.SetInteger("score", points);
        scaleTarget = new Vector2(1, 1) * (expansionFactor * (points % pointsPerPowerLevel) + baseSize * pointsPerPowerLevel) / pointsPerPowerLevel;
        if(tr.localScale.x < scaleTarget.x)
        {
            tr.localScale *= 1 + growthPercentile/100;
        }
        else
        {
            tr.localScale /= 1 + growthPercentile/100;
        }
    }
}
