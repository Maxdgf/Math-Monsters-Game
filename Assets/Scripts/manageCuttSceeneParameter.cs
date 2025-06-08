using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class manageCuttSceeneParameter : MonoBehaviour
{
    [SerializeField]
    Toggle learningToggle;

    private bool isActivated;

    void Start()
    {
        isActivated = learningToggle.isOn;
        PlayerPrefs.SetString("cut_scene_mode", isActivated.ToString());
    }

    public void setCutSceneMode()
    {
        isActivated = learningToggle.isOn;
        PlayerPrefs.SetString("cut_scene_mode", isActivated.ToString());
    }
}
