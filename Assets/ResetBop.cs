using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void FixedUpdate() {
        maintainSize();
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
