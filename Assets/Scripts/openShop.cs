using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class openShop : MonoBehaviour
{
    [SerializeField]
    GameObject[] uiObjects;

    [SerializeField]
    Animator camAnimator;

    [SerializeField]
    Button backToMenuBtn;

    [SerializeField]
    AudioSource soundSource;

    [SerializeField]
    AudioClip sound;

    [SerializeField]
    float uiShowingDelay;

    [SerializeField]
    string animNameStart, animNameEnd;                                                                                                                               

    private VisibilityController vc;
    private SoundController sc;

    void Start()
    {
        vc = gameObject.GetComponent<VisibilityController>();
        sc = gameObject.GetComponent<SoundController>();

        backToMenuBtn.onClick.AddListener(returnToMenu);

        sc.config(soundSource, sound, 1.5f);
    }

    public void shopEvent()
    {
        camAnimator.Play(animNameStart);
        sc.makeSound(soundSource);
        vc.changeVisibilityStateOfObjects(uiObjects, false);
    }

    private void returnToMenu()
    {
        camAnimator.Play(animNameEnd);
        sc.makeSound(soundSource);
        StartCoroutine(showUIAfterDelay(uiShowingDelay));
    }

    private IEnumerator showUIAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);
        vc.changeVisibilityStateOfObjects(uiObjects, true);
    }
}
