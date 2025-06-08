using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skipIntro : MonoBehaviour
{
    [SerializeField]
    int sceneIndex;

    [SerializeField]
    float delay;

    private SceneController sc;

    void Start()
    {
        sc = gameObject.GetComponent<SceneController>();
        sc.setSceneAfterDelay(sceneIndex, delay);
    }
}
