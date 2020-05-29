using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialandLevelSelectMenus : MonoBehaviour {

    public Button tutorialMenuDefaultButton;

    // Start is called before the first frame update
    void Start() {
        tutorialMenuDefaultButton.Select();
    }

    // Update is called once per frame
    void Update() {

    }

    public void ToLevelSelect() {
        SceneManager.LoadScene("LevelSelect", LoadSceneMode.Single);
    }

    public void ToTutorial1() {
        SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
    }

    public void ToTutorial2() {
        SceneManager.LoadScene("Tutorial2", LoadSceneMode.Single);
    }

    public void LoadLevel(int lvl) {
        SceneManager.LoadScene("Level"+lvl.ToString(), LoadSceneMode.Single);
    }

    public void ToMain() {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

}
