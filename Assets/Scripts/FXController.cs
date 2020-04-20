using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXController : MonoBehaviour
{
    public GameObject[] fxObjects;
    private AudioSource[] effects;

    // Start is called before the first frame update
    void Start(){

        effects = new AudioSource[fxObjects.Length];

        for (int i=0; i<fxObjects.Length; i++) {
            effects[i] = fxObjects[i].GetComponent<AudioSource>();
        }

    }

    public void PlayEffect(string choice) {

        switch (choice) {

            case "Fart":
                effects[0].Play();
                break;
            case "Success":
                effects[1].Play();
                break;
            case "Grass":
                effects[2].Play();
                break;
            case "Fire":
                effects[3].Play();
                break;
            case "Wah":
                effects[4].Play();
                break;
        }

    }
}
