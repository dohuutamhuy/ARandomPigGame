using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour {
    public Slider slider;
    public void updateSliderValue(float value) {
        PlayerPrefs.SetFloat("Options_VolumeLevel", value);
    }

    public void Start() {
        slider.value = PlayerPrefs.GetFloat("Options_VolumeLevel", 1f);
    }
}
