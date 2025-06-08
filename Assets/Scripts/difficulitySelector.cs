using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class difficulitySelector : MonoBehaviour
{
    [SerializeField]
    TMP_Dropdown difficulityList;

    [SerializeField]
    TextMeshProUGUI descriptionDifficulityText;

    [SerializeField]
    string[] descriptions;

    void Start()
    {
        int index = difficulityList.value;
        selectDescriptionFromIndex(index);
    }

    public void difficulityClickSelection()
    {
        int index = difficulityList.value;
        selectDescriptionFromIndex(index);
    }

    private void selectDescriptionFromIndex(int index)
    {
        descriptionDifficulityText.text = '-' + descriptions[index];
        PlayerPrefs.SetString("current_dificulity", difficulityList.options[index].text);
    }
}
