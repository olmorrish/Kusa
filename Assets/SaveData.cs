using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SaveData : MonoBehaviour {

    public bool[] levelUnlocks;

    private GameObject playerObj;
    private Player player;

    // Start is called before the first frame update
    void Start(){
        Object.DontDestroyOnLoad(gameObject);

        //only level 1 of 15 is unlocked to start
        levelUnlocks = new bool[15];
        levelUnlocks[0] = true;
        for (int i=1; i<15; i++) {      
            levelUnlocks[i] = false;
        }

        player = null;
    }

    // Update is called once per frame
    void Update(){
        if (player != null && player.levelDone) {
            string completedLevel = SceneManager.GetActiveScene().name;
            int completedLevelNum = int.Parse(completedLevel.Substring(5));   //EX: Level1 -> 1
            if(completedLevelNum < 15)
                levelUnlocks[completedLevelNum] = true;                           //EX: lvl 1 done, unlock lvl2, which is index 1. Unlocks NEXT level.

            Debug.Log("Player completed level " + completedLevelNum + " Unlocked level " + completedLevelNum);
        }
    }

    public void UnlockLevel(int levelNum) {
        levelUnlocks[levelNum] = true;
    }

    private void OnLevelWasLoaded() {

        //on levelselect menu, disable any locked level buttons using the menu interface
        if (SceneManager.GetActiveScene().name.Equals("LevelSelect")) {
            LevelSelectMenu lvlSelectMenu = GameObject.Find("LevelSelectCanvas").GetComponent<LevelSelectMenu>();

            for (int i = 0; i < 15; i++) {
                if (levelUnlocks[i] == false) {
                    lvlSelectMenu.DisableLevelButton(i+1);
                }
            }

            Debug.Log("SaveData disabled locked levels.");
        }

        //on a level, find the player reference so we know when the level is completed
        else if (SceneManager.GetActiveScene().name.Contains("Level") && char.IsDigit(SceneManager.GetActiveScene().name[5])) {
            player = GameObject.Find("PlayerGrass").GetComponent<Player>();
            Debug.Log("SaveData obtained player reference.");
        }

    }
}
