using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LowerTextOnButtonPress : MonoBehaviour
{
    public GameObject buttonTextObject;
    public float distToLower;

    // Start is called before the first frame update
    void Start(){

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnPointerClick() {
        buttonTextObject.transform.position = new Vector3(buttonTextObject.transform.position.x, buttonTextObject.transform.position.y - distToLower, buttonTextObject.transform.position.z);
    }
}
