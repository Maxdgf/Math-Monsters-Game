using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class manageLearningParameter : MonoBehaviour
{
    [SerializeField]
    Toggle learningToggle;

    private bool isActivated;

    void Start()
    {
        isActivated = learningToggle.isOn;
        PlayerPrefs.SetString("learning_mode", isActivated.ToString());
    }

    public void setLearningMode()
    {
        isActivated = learningToggle.isOn;
        PlayerPrefs.SetString("learning_mode", isActivated.ToString());
    }
}
