using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class cutSceneCameraController : MonoBehaviour
{
    [SerializeField]
    private string doorTrig, bedTrig, lightTorchTrig, tabletAnimation, doorAnimation, monstersAnimation, tabletAnimation2, sleepAnimation, doorMovingAnimation, playerScares, playerOpenEyes, playerJumpsFromBedToDoor, lightTorch, playerGetsTabletAndActivateLightTorch, tabletComeToPlayer;

    [SerializeField]
    private Animator tabletAnimator, doorAnimator, monstersAnimator, sleepAnimator, cameraAnimator, lightTorchAnimator, tabletOnTheFloorAnimator;

    [SerializeField]
    private AudioSource doorSound, tabletSound, bedSound, monstersSound, cutSceneCameraWalkSound, monster;

    [SerializeField]
    private AudioClip doorMoving, doorDefault;

    [SerializeField]
    private Material nightMaterial;

    [SerializeField]
    private float showDialogDelay, bedTabletDelay, bedMonstersDelay, bedTabletDelay2, sleepDelay, changeSkyBoxMaterialDelay, showTextGreenMonsterWinsDelay, playerScaresDelay, playerJumpsFromBedToDoorDelay, playerGetsTabletAndActivateLightTorchDelay, endDelay, showUIdelay;

    [SerializeField]
    private TextMeshProUGUI dialogText;

    [SerializeField]
    private GameObject[] objectsToHide;

    [SerializeField]
    private GameObject[] objectsToShow;

    [SerializeField]
    private GameObject[] lights;

    [SerializeField]
    private GameObject[] objectsToDestroyInEnd;

    [SerializeField]
    private GameObject[] uiObjects;

    [SerializeField]
    private GameObject playerBody, dialogView, monsterSpawner, sleepPanel, directionalLight;

    private VisibilityController vc;
    private ObjDestroyer od;
    private Camera cutSceneCam;
    private bool isDoorOpeningSecondary = false;

    void Start()
    {
        vc = gameObject.GetComponent<VisibilityController>();
        od = gameObject.GetComponent<ObjDestroyer>();
        cutSceneCam = gameObject.GetComponent<Camera>();

        StartCoroutine(showDialogPanel(showDialogDelay));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(doorTrig))
        {
            if (doorSound.clip == doorMoving)
            {
                doorSound.clip = doorDefault;
            }

            doorAnimator.Play(doorAnimation);
            doorSound.Play();

            if (!isDoorOpeningSecondary)
            {
                dialogText.text = "I like my room! I can do anything I want here.";
            } 
            else
            {
                dialogText.text = "Heeeeeeey! Who's there?? Heeeeey! Get out of here!";
                Destroy(GameObject.Find("doorTrig"));
                cutSceneCameraWalkSound.Stop();
            }
        } 
        else if (other.CompareTag(bedTrig))
        {
            cutSceneCameraWalkSound.Stop();

            sleepPanel.SetActive(true);

            dialogText.text = "I'm so tired...";

            bedSound.Play();

            StartCoroutine(playTabletAnimationAfterDelay(bedTabletDelay, tabletAnimation));
            StartCoroutine(playHideTabletAnimationAfterDelay(bedTabletDelay2, tabletAnimation2));
            StartCoroutine(playMonstersAnimationAfterDelay(bedMonstersDelay, monstersAnimation));
            StartCoroutine(showTextGreenMonsterWins(showTextGreenMonsterWinsDelay));
            StartCoroutine(sleepPlayerAfterDelay(sleepDelay, sleepAnimation));
            StartCoroutine(changeSkyBoxToNight(changeSkyBoxMaterialDelay, nightMaterial));
            StartCoroutine(playPlayerScresMonsterAfterDelay(playerScaresDelay, playerScares));
            StartCoroutine(playPlayerJumpsFromBedAfterDelay(playerJumpsFromBedToDoorDelay, playerJumpsFromBedToDoor));
            StartCoroutine(playerGetsTabletAndActivateLightTorchAfterDelay(playerGetsTabletAndActivateLightTorchDelay, playerGetsTabletAndActivateLightTorch, tabletComeToPlayer));
            StartCoroutine(endCutSceneRequest(endDelay));
            StartCoroutine(showUIandHideDialogView(showUIdelay));
        } 
        else if (other.CompareTag(lightTorchTrig))
        {
            if (isDoorOpeningSecondary)
            {
                lightTorchAnimator.Play(lightTorch);
            }
        }
    }

    private IEnumerator showDialogPanel(float time)
    {
        yield return new WaitForSeconds(time);
        dialogView.SetActive(true);
        cutSceneCameraWalkSound.Play();
    }

    private IEnumerator playTabletAnimationAfterDelay(float time, string anim)
    {
        yield return new WaitForSeconds(time);
        tabletSound.Play();
        tabletAnimator.Play(anim);
        dialogText.text = "What!? Really? Oh noooo. That's not enough yet. How will I prepare for the evening?";
    }

    private IEnumerator playHideTabletAnimationAfterDelay(float time, string anim)
    {
        yield return new WaitForSeconds(time);
        tabletAnimator.Play(anim, -1, 0f);
        dialogText.text = "It's impossible. :(((";
    }

    private IEnumerator playMonstersAnimationAfterDelay(float time, string anim)
    {
        yield return new WaitForSeconds(time);
        monstersSound.Play();
        monstersAnimator.Play(anim);
        dialogText.text = "Ok, I'll play with the monsters for now. Get it, get it, get it!";
    }

    private IEnumerator showTextGreenMonsterWins(float time)
    {
        yield return new WaitForSeconds(time);
        dialogText.text = "Hahahahaha... Green monster wins! I'll have the whole collection soon.";
    }

    private IEnumerator sleepPlayerAfterDelay(float time, string anim)
    {
        yield return new WaitForSeconds(time);
        sleepAnimator.Play(anim);

        dialogText.text = "I'm feeling sleepy...";

        isDoorOpeningSecondary = true;

        vc.changeVisibilityStateOfObjects(lights, false);
        vc.changeVisibilityStateOfObjects(objectsToShow, true);
    }

    private IEnumerator changeSkyBoxToNight(float time, Material sky)
    {
        yield return new WaitForSeconds(time);
        RenderSettings.skybox = sky;
        dialogText.text = "";
        directionalLight.SetActive(false);
    }

    private IEnumerator playPlayerScresMonsterAfterDelay(float time, string anim)
    {
        yield return new WaitForSeconds(time);
        vc.changeVisibilityStateOfObjects(objectsToHide, false);

        cameraAnimator.Play(anim);
        sleepAnimator.Play(playerOpenEyes);

        doorSound.clip = doorMoving;

        doorSound.Play();
        doorAnimator.Play(doorMovingAnimation);

        dialogText.text = "What is this!!?? Who's at my house?";
    }

    private IEnumerator playPlayerJumpsFromBedAfterDelay(float time, string anim)
    {
        yield return new WaitForSeconds(time);
        cameraAnimator.Play(anim);
        dialogText.text = "I must to check it out!";
        cutSceneCameraWalkSound.pitch = 1.6f;
        cutSceneCameraWalkSound.Play();
    }

    private IEnumerator playerGetsTabletAndActivateLightTorchAfterDelay(float time, string anim, string tabletAnim)
    {
        yield return new WaitForSeconds(time);
        cameraAnimator.Play(anim);
        tabletOnTheFloorAnimator.Play(tabletAnim);
        dialogText.text = "What is this!? What's wrong with the light and why is my tablet here?";
    }

    private IEnumerator endCutSceneRequest(float time)
    {
        yield return new WaitForSeconds(time);
        od.destroyObjects(objectsToDestroyInEnd);
        monsterSpawner.SetActive(true);
        cutSceneCam.enabled = false;
        playerBody.SetActive(true);
        monster.Play();
        dialogText.text = "What do I need to solve this math, but for what purpose? I think I understood...";
    }

    private IEnumerator showUIandHideDialogView(float time)
    {
        yield return new WaitForSeconds(time);
        vc.changeVisibilityStateOfObjects(uiObjects, true);
        dialogText.text = "";
        dialogView.SetActive(false);
        Destroy(gameObject);
    }
}