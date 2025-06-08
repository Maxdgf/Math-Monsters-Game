using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class shopItemsAndStatsLoader : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI crystallsView, safeShieldCount, freezingCount, helpCount;

    void Start()
    {
        int money = PlayerPrefs.GetInt("all_crystalls", 0);
        int safeShields = PlayerPrefs.GetInt("SaveShield", 0);
        int freezings = PlayerPrefs.GetInt("Freezing", 0);

        crystallsView.text = money.ToString();
        safeShieldCount.text = safeShields.ToString();
        freezingCount.text = freezings.ToString();
    }
}

