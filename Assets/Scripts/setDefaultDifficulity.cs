using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class setDefaultDifficulity : MonoBehaviour
{
    [SerializeField]
    TMP_Dropdown difficulityList;

    [SerializeField]
    int difficulityIndex;

    private int elementsCount;

    void Start()
    {
        elementsCount = difficulityList.options.Count;

        if (difficulityIndex <= elementsCount)
        {
            difficulityList.value = difficulityIndex;
        }
        else if (difficulityIndex > elementsCount - 1)
        {
            difficulityList.value = elementsCount - 1;
            Debug.LogWarning($"Default game difficulity: difficulity index out of bounds! {difficulityIndex}");
        }
        else if (difficulityIndex < 0)
        {
            difficulityList.value = 0;
            Debug.LogWarning($"Default game difficulity: difficulity index out of bounds! {difficulityIndex}");
        }
    }
}