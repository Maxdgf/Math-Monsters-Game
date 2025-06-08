using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class buyItem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI money, price, itemCountView;

    [SerializeField]
    private GameObject itemCard;

    [SerializeField]
    string itemName;

    [SerializeField]
    private float itemCardBlinkDelay;

    [SerializeField]
    private AudioSource buySound;

    [SerializeField]
    private AudioClip[] buySounds;

    private int itemCount;
    private Color startItemCardColor;
    private Image image;

    void Start()
    {
        itemCount = PlayerPrefs.GetInt(itemName, 0);

        image = itemCard.GetComponent<Image>();
        startItemCardColor = image.color;
    }

    public void buyThis()
    {
        int crystalls = Convert.ToInt32(money.text);
        int priceItem = Convert.ToInt32(price.text);

        if (crystalls >= priceItem)
        {
            itemCount++;
            int newMoneyCount = crystalls -= priceItem;

            if (buySound.clip != buySounds[0])
            {
                buySound.clip = buySounds[0]; //buy item sound
            }

            buySound.Play();

            string newMoney = newMoneyCount.ToString();

            PlayerPrefs.SetInt(itemName, itemCount);
            PlayerPrefs.SetInt("all_crystalls", newMoneyCount);

            money.text = newMoney;
            itemCountView.text = itemCount.ToString();
        }
        else
        {
            if (buySound.clip != buySounds[1])
            {
                buySound.clip = buySounds[1]; //no money for buy item sound
            }

            buySound.Play();

            image.color = Color.red;
            StartCoroutine(itemCardBlink(itemCardBlinkDelay, startItemCardColor, image));
        }
    }

    private IEnumerator itemCardBlink(float time, Color startColor, Image img)
    {
        yield return new WaitForSeconds(time);
        img.color = startColor;
    }
}
