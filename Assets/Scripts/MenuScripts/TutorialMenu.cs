using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialMenu : MonoBehaviour{

    public Button tutorialDefaultButton;

    public void Start() {
        tutorialDefaultButton.Select();
    }

    public void ToLevelSelect() {
        SceneManager.LoadScene("LevelSelect", LoadSceneMode.Single);
    }

    public void ToTutorial2() {
        SceneManager.LoadScene("Tutorial2", LoadSceneMode.Single);
    }
}
