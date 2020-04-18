using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManureDespawn : MonoBehaviour
{

    public float lifeSpan;
    private float diesAtTime;

    private Animator anim;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Awake() {
        diesAtTime = Time.time + lifeSpan;
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update(){

        if ((diesAtTime - Time.time) < (lifeSpan / 3)) {    //if there's only 1/3 of the lifespan left
            anim.SetBool("flashing", true);
        }

        if (Time.time > diesAtTime) {
            GameObject.Destroy(gameObject);
        }
    }

    /* Toggle Sprite
     * Toggles the spriterender component to make the powerup flash before despawning
     * Called as an animation even so the flashrate is changeable
     */
    public void ToggleSprite() {
        sprite.enabled = !sprite.enabled;
    }
}
