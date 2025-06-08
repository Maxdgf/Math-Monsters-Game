using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class lightTorchManager : MonoBehaviour
{
    [SerializeField]
    GameObject lightTorch;

    [SerializeField]
    AudioSource clickSound;

    [SerializeField]
    AudioClip click;

    [SerializeField]
    Slider batteryLevel;

    [SerializeField]
    Image sliderElement;

    [SerializeField]
    TextMeshProUGUI chargeView;

    [SerializeField]
    int batteryLeft;

    [SerializeField]
    float batteryLeftDelay;

    private Light lt;
    private bool isActivated = false;
    private bool isEnergyInBatteryLeft = false;
    private int batteryLevelValue;

    void Start()
    {
        lt = lightTorch.GetComponent<Light>();
        clickSound.clip = click;

        batteryLevelValue = (int)batteryLevel.value;

        if (lt.enabled)
        {
            StartCoroutine(calculateBatteryLevel(batteryLeftDelay, batteryLeft));
        }
    }

    void Update()
    {
        chargeView.text = $"{batteryLevelValue}%";

        if (batteryLevelValue == 0)
        {
            isEnergyInBatteryLeft = true;
            lt.enabled = false;
            StopAllCoroutines();
        }

        checkSliderLevel(batteryLevel, sliderElement);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!isEnergyInBatteryLeft)
            {
                if (!isActivated)
                {
                    lt.enabled = false;
                    isActivated = true;
                    StopAllCoroutines();
                }
                else
                {
                    lt.enabled = true;
                    isActivated = false;
                    StartCoroutine(calculateBatteryLevel(batteryLeftDelay, batteryLeft));
                }
            }

            clickSound.Play();
        }

        batteryLevel.value = batteryLevelValue;
    }

    private void checkSliderLevel(Slider slider, Image sliderElement)
    {
        int value = (int)slider.value;

        if (value <= 25)
        {
            sliderElement.color = Color.red;
        }
        else if (value <= 75)
        {
            sliderElement.color = Color.yellow;
        }
        else if (value <= 100)
        {
            sliderElement.color = Color.green;
        }
    }

    private IEnumerator calculateBatteryLevel(float time, int left)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            batteryLevelValue -= left;
        }
    }
}
