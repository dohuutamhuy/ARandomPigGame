using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour {
    // Start is called before the first frame update
    static BackgroundMusic instance = null;
    void Awake() {
        if (instance != null) {
            Destroy(gameObject); //otherwise multiple music will play
        } else {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }

    void Update() {
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Options_VolumeLevel", 1f);
    }
}
