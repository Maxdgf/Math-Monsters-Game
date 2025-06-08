using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideLearningPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject learningPanel, lightCorridor, monsterSpawner;

    [SerializeField]
    private GameObject[] uiObjects;

    private string cutSceneState;
    private bool isActiveCutScene;
    private VisibilityController vc;

    void Start()
    {
        cutSceneState = PlayerPrefs.GetString("cut_scene_mode");
        isActiveCutScene = Convert.ToBoolean(cutSceneState);
        vc = gameObject.GetComponent<VisibilityController>();
    }

    public void HideLearningPanel()
    {
        if (!isActiveCutScene)
        {
            learningPanel.SetActive(false);
            Time.timeScale = 1f;

            vc.changeVisibilityStateOfObjects(uiObjects, true);
            lightCorridor.SetActive(false);
            monsterSpawner.SetActive(true);

            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            learningPanel.SetActive(false);
            Time.timeScale = 1f;

            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
