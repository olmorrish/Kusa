using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicFunctions : MonoBehaviour
{
    public GameObject playerReference;
    GameObject[] allSheep;

    private float nextGrassBeatTime;
    private float nextSheepBeatTime;
    private bool poseChangeNextBeat;

    public ParticleSystem grassPSystem;
    private ParticleSystem[] sheepPSystems;
    public int burstAmount;

    public bool triggerForTestingNotPermanent;

    //Sound Effects
    public GameObject soundEffectParentObject;
    private FXController fxController;

    // Start is called before the first frame update
    void Start() {
        allSheep = GameObject.FindGameObjectsWithTag("Sheep");

        sheepPSystems = new ParticleSystem[allSheep.Length];
        for(int i=0; i<sheepPSystems.Length; i++) {
            sheepPSystems[i] = allSheep[i].GetComponentInChildren<ParticleSystem>();
        }

        fxController = soundEffectParentObject.GetComponent<FXController>();

        nextGrassBeatTime = Time.time + 1;
        nextSheepBeatTime = Time.time + 1.5f;
        poseChangeNextBeat = false;
    }

    // Update is called once per frame
    void Update(){
        if(Time.time >= nextGrassBeatTime) {
            Beat1or3();
            nextGrassBeatTime = Time.time + 1;
        }

        if (Time.time >= nextSheepBeatTime) {
            if (poseChangeNextBeat) {
                Beat2();
                poseChangeNextBeat = false;
            }
            else {
                Beat4();
                poseChangeNextBeat = true;
            }
            nextSheepBeatTime = Time.time + 1;
        }

        if (triggerForTestingNotPermanent) {
            TriggerGrassParticleBurst(burstAmount);
            triggerForTestingNotPermanent = false;
        }
    }

    /*
     * 
     */
    public void Beat1or3() {
        TriggerGrassParticleBurst(burstAmount);                     //shoots kanji from grass
        playerReference.GetComponent<Player>().UpdatePowerLevel();

        //get ref to all patches in scene right now
        GameObject[] allPatches = GameObject.FindGameObjectsWithTag("Patch");
        //Debug.Log(allPatches.Length);

        //give off all particle bursts
        ParticleSystem[] allPatchPSystems = new ParticleSystem[allPatches.Length];
        for (int i = 0; i < allPatchPSystems.Length; i++) {
            allPatchPSystems[i] = allPatches[i].GetComponentInChildren<ParticleSystem>();
            if (allPatchPSystems[i] != null) {
                allPatchPSystems[i].Play();
                allPatchPSystems[i].emission.SetBurst(0, new ParticleSystem.Burst(0, 1));
            }
        }

        //do all the bops
        for (int i = 0; i < allPatches.Length; i++) {
            allPatches[i].GetComponent<Transform>().localScale *= 1.1f; //the bop will be reset by the patch script on its next fixed update
        }

        //bop powerups
        GameObject[] allManure = GameObject.FindGameObjectsWithTag("PowerUp");
        for (int i = 0; i < allManure.Length; i++) {
            allManure[i].GetComponent<Transform>().localScale *= 1.1f; //the bop will be reset by the patch script on its next fixed update
        }
    }

    public void Beat2() {
        // kanji from sheep
        for (int i = 0; i < sheepPSystems.Length; i++) {
            TriggerSheepParticleBurst(burstAmount, sheepPSystems[i]);
        }

        ////do all the bops for obstacles
        //GameObject[] allObstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        //for (int i = 0; i < allObstacles.Length; i++) {
        //    allObstacles[i].GetComponent<Transform>().localScale *= 1.1f; //the bop will be reset by the patch script on its next fixed update
        //}

        

    }


    public void Beat4() {

        //do all the bops for obstacles
        GameObject[] allObstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        for (int i = 0; i < allObstacles.Length; i++) {
            allObstacles[i].GetComponent<Transform>().localScale *= 1.1f; //the bop will be reset by the patch script on its next fixed update
        }

        //sheep kanji
        for (int i = 0; i < sheepPSystems.Length; i++) {
            TriggerSheepParticleBurst(burstAmount, sheepPSystems[i]);
        }

        //change the pose of every sheep in the scene
        fxController.PlayEffect("Wah");
        for (int i=0; i<allSheep.Length; i++) {
            allSheep[i].GetComponent<Sheep>().NewPose();
            allSheep[i].GetComponent<Transform>().localScale *= 1.1f;
        }
    }


    public void TriggerSheepParticleBurst(int amount, ParticleSystem pSys) {
        if (pSys != null) {
            pSys.Play();
            pSys.emission.SetBurst(0, new ParticleSystem.Burst(0, amount));
            return;
        }
        Debug.Log("Didn't set the particle system dummy.");
    }

    public void TriggerGrassParticleBurst(int amount) {
        if (grassPSystem != null) {
            grassPSystem.Play();
            grassPSystem.emission.SetBurst(0, new ParticleSystem.Burst(0, amount));
            return;
        }
        Debug.Log("Didn't set the particle system dummy.");
    }



}
