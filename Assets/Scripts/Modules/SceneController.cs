using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void setScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void setSceneAsync(int index, float delay, TextMeshProUGUI text)
    {
        StartCoroutine(loadSceneAsync(delay, index, text));
    }

    private IEnumerator loadSceneAsync(float time, int index, TextMeshProUGUI progressView)
    {
        yield return new WaitForSeconds(time);

        AsyncOperation operation = SceneManager.LoadSceneAsync(index);

        while (!operation.isDone)
        {
            float percentage = calculateProgressPerccentage(operation.progress);
            progressView.text = "Loading " + Mathf.Round(percentage) + "%";
            yield return null;
        }
    }

    private float calculateProgressPerccentage(float progress)
    {
        //return progress * 100;
        return (progress + 0.1f) * 100; //operation.progress method always shows final percentage 0.9f (90%), but i solved it by simple addition 0.1f to operation.progress method value. 
    }

    public void setSceneAfterDelay(int index, float delay)
    {
        StartCoroutine(loadSceneAfterDelay(delay, index));
    }

    private IEnumerator loadSceneAfterDelay(float time, int index)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(index);
    }
}
