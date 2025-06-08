using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameParametersConfigurator : MonoBehaviour
{
    [SerializeField]
    GameObject learningPanel, cutSceneCamera, playerBody, monsterSpawner, lightCorridor;

    [SerializeField]
    private GameObject[] uiObjects;

    [SerializeField]
    private Material nightSky;

    private string learningPanelState;
    private string cutSceneState;

    private bool isActivatedLearningPanel;
    private bool isActivatedCutScene;

    private VisibilityController vc;
    
    void Start()
    {
        vc = gameObject.GetComponent<VisibilityController>();

        learningPanelState = PlayerPrefs.GetString("learning_mode");
        cutSceneState = PlayerPrefs.GetString("cut_scene_mode");

        isActivatedLearningPanel = Convert.ToBoolean(learningPanelState);
        isActivatedCutScene = Convert.ToBoolean(cutSceneState);

        Debug.Log("Learning game panel visibility state: " + learningPanelState);

        if (isActivatedLearningPanel)
        {
            learningPanel.SetActive(true);
            Time.timeScale = 0f;
        } 
        else
        {
            learningPanel.SetActive(false);
        }

        PlayerPrefs.SetString("learning_mode", "false");

        if (isActivatedCutScene)
        {
            cutSceneCamera.SetActive(true);
        }
        else
        {
            Destroy(cutSceneCamera);
            Destroy(GameObject.Find("doorTrig")); //destroy a door animation trigger
            playerBody.SetActive(true); 
            Cursor.lockState = CursorLockMode.Locked;
            RenderSettings.skybox = nightSky;
        }

        PlayerPrefs.SetString("cut_scene_mode", "false");

        if (!isActivatedLearningPanel && !isActivatedCutScene)
        {
            vc.changeVisibilityStateOfObjects(uiObjects, true);

            monsterSpawner.SetActive(true);
            lightCorridor.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            RenderSettings.skybox = nightSky;

            Destroy(cutSceneCamera);
        }
    }
}
