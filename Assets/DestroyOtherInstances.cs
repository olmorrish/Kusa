using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOtherInstances : MonoBehaviour
{
    void Update(){
        GameObject otherInstance = GameObject.FindGameObjectWithTag("DuplicateMusic");
        if (otherInstance != null) {
            //Debug.Log("Found: " + otherInstance.name);
            Destroy(otherInstance);
        }
    }

}
