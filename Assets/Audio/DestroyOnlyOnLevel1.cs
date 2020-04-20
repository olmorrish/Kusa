using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyOnlyOnLevel1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        DontDestroyOnLoad(this.gameObject);

    }

    // Update is called once per frame
    void Update(){
        if (SceneManager.GetActiveScene().name.Equals("Level1")) {
            GameObject.Destroy(gameObject);
        }

    }
}
