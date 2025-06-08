using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLaunchSetup : MonoBehaviour
{
    [SerializeField]
    Material startSkyBox;

    void Start()
    {
        RenderSettings.skybox = startSkyBox;
    }
}
