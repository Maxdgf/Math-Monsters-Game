using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityController : MonoBehaviour
{
    public void changeVisibilityStateOfObjects(GameObject[] objects, bool state)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(state);
        }
    }
}
