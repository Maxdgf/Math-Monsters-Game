using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class crystallCounter : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI countView;

    [SerializeField]
    Animator crystallIcon;

    [SerializeField]
    string crystallIconAnimation;

    [SerializeField]
    AudioSource soundSource;

    private int count;
    private int crystalls;

    void Start()
    {
        crystalls = PlayerPrefs.GetInt("all_crystalls", 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        count++;
        PlayerPrefs.SetInt("all_crystalls", crystalls + count);
        PlayerPrefs.SetInt("catched_crystalls", count);
        countView.text = count.ToString();

        soundSource.Play();
        crystallIcon.Play(crystallIconAnimation, -1, 0f);
    }
}