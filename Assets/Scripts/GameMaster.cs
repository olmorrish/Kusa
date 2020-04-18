using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{

    public int currentPower;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdatePowerLevel() {
        GameObject[] patches = GameObject.FindGameObjectsWithTag("Patch");
        int numPatches = patches.Length;
        //TODO
    }
}
