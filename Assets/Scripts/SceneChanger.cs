using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    GameObject[] uiObjects;

    [SerializeField]
    GameObject adviceView;

    [SerializeField]
    TextMeshProUGUI loadingText;

    [SerializeField]
    Animator cameraAnimator;

    [SerializeField]
    AudioSource soundSource;

    [SerializeField]
    AudioClip sound;

    [SerializeField]
    string cameraAnimName;

    [SerializeField]
    int sceneIndex;

    [SerializeField]
    bool asyncLoad;

    private VisibilityController vc;
    private SceneController scScene;
    private SoundController scSound;

    void Start()
    {
        vc = gameObject.GetComponent<VisibilityController>();
        scSound = gameObject.GetComponent<SoundController>();
        scScene = gameObject.GetComponent<SceneController>();

        scSound.config(soundSource, sound, 1.5f);
    }

    public void changeScene()
    {
        if (sceneIndex >= 0)
        {
            scSound.makeSound(soundSource);

            loadingText.text = "Loading 0%";

            adviceView.SetActive(true);

            cameraAnimator.Play(cameraAnimName);

            if (uiObjects != null)
            {
                vc.changeVisibilityStateOfObjects(uiObjects, false);
            }

            if (!asyncLoad)
            {
                loadingText.text = "Loading...";
                scScene.setSceneAfterDelay(sceneIndex, 5);
            } 
            else
            {
                if (loadingText != null)
                {
                    scScene.setSceneAsync(sceneIndex, 5, loadingText);
                }
            }
        } 
        else
        {
            Debug.LogError("SceneChanger: scene index less zero!");
        }
    }
}
