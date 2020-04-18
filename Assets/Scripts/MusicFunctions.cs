using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicFunctions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     * 
     */
    public void Beat1() {
        //TODO kanji from grass
    }

    public void Beat2() {
        //TODO kanji from sheep
    }

    public void Beat3() {
        //TODO kanji from grass
    }

    public void Beat4() {
        //TODO kanji from sheep

        //change the pose of every sheep in the scene
        GameObject[] allSheep = GameObject.FindGameObjectsWithTag("Sheep");
        for (int i=0; i<allSheep.Length; i++) {
            allSheep[i].GetComponent<Sheep>().NewPose();
        }
    }





}
