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
    public ParticleSystem sheepPSystem;
    public int burstAmount;

    public bool triggerForTestingNotPermanent;

    // Start is called before the first frame update
    void Start() {
        allSheep = GameObject.FindGameObjectsWithTag("Sheep");

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
    }

    public void Beat2() {
        //TODO kanji from sheep
        TriggerSheepParticleBurst(burstAmount);
    }


    public void Beat4() {
        //TODO kanji from sheep
        TriggerSheepParticleBurst(burstAmount);

        //change the pose of every sheep in the scene
        for (int i=0; i<allSheep.Length; i++) {
            allSheep[i].GetComponent<Sheep>().NewPose();
        }
    }


    public void TriggerSheepParticleBurst(int amount) {

        if (sheepPSystem != null) {
            sheepPSystem.Play();
            sheepPSystem.emission.SetBurst(0, new ParticleSystem.Burst(0, amount));
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
