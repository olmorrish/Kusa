using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patch : MonoBehaviour
{

    private Animator anim;

    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D col) {

        if (col.gameObject.CompareTag("Sheep")) {
            anim.SetBool("killed", true);
        }
    }

    public void DeleteSelf() {
        GameObject.Destroy(gameObject);
    }
}
