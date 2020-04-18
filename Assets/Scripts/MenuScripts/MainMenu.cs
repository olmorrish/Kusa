using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button mainMenuDefaultButton;

    // Start is called before the first frame update
    void Start()
    {
        mainMenuDefaultButton.Select();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadHowToPlay() {
        SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
    }

}
