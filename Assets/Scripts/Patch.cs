using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patch : MonoBehaviour
{

    private Animator anim;

    //Sound
    public GameObject soundEffectParentObject;
    private FXController fxController;

    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
        soundEffectParentObject = GameObject.Find("SoundEffects");
        fxController = soundEffectParentObject.GetComponent<FXController>();
    }

    private void FixedUpdate() {
        maintainSize();
    }

    private void OnCollisionEnter2D(Collision2D col) {

        //play the fire animation (which then deletes the patch) if a sheep touches it
        if (col.gameObject.CompareTag("Sheep")) {
            fxController.PlayEffect("Fire");
            anim.SetBool("killed", true);
        }

        //destroy any patches that overlap with this patch; the frontmost of the two will be kept
        if (col.gameObject.CompareTag("Patch")) {

            float myZPos = transform.position.z;
            float otherPatchZPos = col.gameObject.transform.position.z;

            if(otherPatchZPos >= myZPos) {
                GameObject.Destroy(col.gameObject);
            }

        }
    }

    public void DeleteSelf() {
        GameObject.Destroy(gameObject);
    }

    /// <summary>
    /// returns size to normal after on Beat expansion
    /// </summary>
    private void maintainSize() {
        if (transform.localScale.x > 1) {
            transform.localScale -= new Vector3(0.02f, 0.02f, 0.02f);
        }
    }
}
