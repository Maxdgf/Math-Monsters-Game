using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class gameAdviceGenerator : MonoBehaviour
{
    [SerializeField]
    string[] advices;

    [SerializeField]
    TextMeshProUGUI adviceView;

    void Start()
    {
        if (advices.Length != 0 && advices != null)
        {
            int index = Random.Range(0, advices.Length);
            adviceView.text = advices[index];
        }
    }
}
