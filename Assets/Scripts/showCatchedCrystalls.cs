using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class showCatchedCrystalls : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI crystallsView;

    void Start()
    {
        crystallsView.text = '+' + PlayerPrefs.GetInt("catched_crystalls", 0).ToString() + " crystalls";
        PlayerPrefs.SetInt("catched_crystalls", 0);
    }
}
