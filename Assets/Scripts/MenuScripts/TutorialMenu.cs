using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialMenu : MonoBehaviour {

    public Button tutorialMenuDefaultButton;

    // Start is called before the first frame update
    void Start() {
        tutorialMenuDefaultButton.Select();
    }

    // Update is called once per frame
    void Update() {

    }

    public void StartGame() {
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }

}
