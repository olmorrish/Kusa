using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyOnLevelLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        DontDestroyOnLoad(this.gameObject);

    }

    // Update is called once per frame
    void Update(){

        for(int i=0; i<=15; i++) {
            if (SceneManager.GetActiveScene().name.Equals("Level" + i)) {
                GameObject.Destroy(gameObject);
        }

        }

    }
}
