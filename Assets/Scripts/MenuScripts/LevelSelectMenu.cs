using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectMenu : MonoBehaviour {

    public Button levelSelectDefaultButton;

    //public GameObject saveDataObject;
    //private SaveData saveData;

    // Start is called before the first frame update
    void Start() {
        levelSelectDefaultButton.Select();
    }

    public void DisableLevelButton(int lvl) {
        Button lvlButton = GameObject.Find("Lvl"+lvl).GetComponent<Button>();
        lvlButton.interactable = false;
        lvlButton.GetComponent<Image>().color = new Color(200f/255f,30f/255f,0f,180f/255f); 
    }

    public void LoadLevel(int lvl) {
        SceneManager.LoadScene("Level"+lvl.ToString(), LoadSceneMode.Single);
    }

    public void ToMain() {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void ToTutorial() {
        SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
    }
}
