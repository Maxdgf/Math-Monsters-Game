using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class bonusesEngine : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI saveShields, freezings;

    [SerializeField]
    private GameObject shieldObject, bonusTimeBar, bonusesBar, monsterSpawner;

    [SerializeField]
    private Slider bonusSlider;

    private int shieldTime = 15;
    private int freezeTime = 20;
    private bool isBonusUsingNow = false;
    private int shield;
    private int freeze;

    void Start()
    {
        shield = PlayerPrefs.GetInt("SaveShield");
        freeze = PlayerPrefs.GetInt("Freezing");

        saveShields.text = shield.ToString();
        freezings.text = freeze.ToString();
    }

    void Update()
    {
        if (!isBonusUsingNow)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (shield > 0)
                {
                    shieldObject.SetActive(true);
                    bonusTimeBar.SetActive(true);
                    bonusesBar.SetActive(false);

                    bonusSlider.maxValue = shieldTime;
                    bonusSlider.value = shieldTime;
                    isBonusUsingNow = true;

                    int newShieldCount = shield -= 1;

                    PlayerPrefs.SetInt("SaveShield", newShieldCount);

                    saveShields.text = newShieldCount.ToString();

                    StartCoroutine(calculateShieldTime(1));
                }
            } 
            else if (Input.GetKeyDown(KeyCode.S))
            {
                monsterSpawner.SetActive(false);
                bonusTimeBar.SetActive(true);
                bonusesBar.SetActive(false);

                bonusSlider.maxValue = freezeTime;
                bonusSlider.value = freezeTime;
                isBonusUsingNow = true;

                int newFreezeCount = shield -= 1;

                PlayerPrefs.SetInt("Freezing", newFreezeCount);

                freezings.text = newFreezeCount.ToString();

                GameObject[] monsters = GameObject.FindGameObjectsWithTag("Enemy");

                if (monsters.Length == 0)
                {
                    Debug.Log("All monsters deleted with name: " + name);
                    return;
                }

                foreach (GameObject monster in monsters)
                {
                    NavMeshAgent agent = monster.GetComponent<NavMeshAgent>();
                    agent.isStopped = true;
                }

                StartCoroutine(resetFreezeAfterDelay(1, monsters));
            } 
        }
    }

    private IEnumerator calculateShieldTime(float time)
    {
        while (shieldTime > 0)
        {
            yield return new WaitForSeconds(time);
            shieldTime--;
            bonusSlider.value = shieldTime;
        }

        bonusTimeBar.SetActive(false);
        bonusesBar.SetActive(true);
        shieldObject.SetActive(false);
        isBonusUsingNow = false;
    }

    private IEnumerator resetFreezeAfterDelay(float time, GameObject[] monsters)
    {
        while (freezeTime > 0)
        {
            yield return new WaitForSeconds(time);
            freezeTime--;
            bonusSlider.value = freezeTime;
        }

        foreach (GameObject monster in monsters)
        {
            NavMeshAgent agent = monster.GetComponent<NavMeshAgent>();
            agent.isStopped = false;
        }

        bonusTimeBar.SetActive(false);
        bonusesBar.SetActive(true);
        monsterSpawner.SetActive(true);
        isBonusUsingNow = false;
    }
}
