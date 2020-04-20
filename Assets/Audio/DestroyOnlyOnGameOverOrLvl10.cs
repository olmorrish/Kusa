using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyOnlyOnGameOverOrLvl10 : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        DontDestroyOnLoad(this.gameObject);

    }

    // Update is called once per frame
    void Update() {

        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName.Equals("GameOver") || sceneName.Equals("Level10")) {
            GameObject.Destroy(gameObject);
        }

    }
}
