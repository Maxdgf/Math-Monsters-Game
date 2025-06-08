using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjDestroyer : MonoBehaviour
{
    public void destroyObjects(GameObject[] objects)
    {
        for (int i = 0; i < objects.Length - 1; i++)
        {
            Destroy(objects[i]);
        }
    }
}
