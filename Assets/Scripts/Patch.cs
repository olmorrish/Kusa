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

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.gameObject.CompareTag("Sheep")) {
            anim.SetBool("killed", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
