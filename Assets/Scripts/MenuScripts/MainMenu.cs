using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button mainMenuDefaultButton;

    // Start is called before the first frame update
    void Start(){
        mainMenuDefaultButton.Select();
    }

    public void LoadHowToPlay() {
        SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void LoadLevelSelect() {
        SceneManager.LoadScene("LevelSelect", LoadSceneMode.Single);
    }

}
