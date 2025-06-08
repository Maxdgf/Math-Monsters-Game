using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjSpawnerController : MonoBehaviour
{ 
    public void StartSpawnObj(GameObject target_obj, GameObject spawn_obj, int obj_count, float spawn_delay)
    {
        Transform target_transform = target_obj.transform;

        for (int i = 0; i < obj_count; i++)
        {
            StartCoroutine(SpawnObj(spawn_obj, target_transform, spawn_delay));
        }
    }

    IEnumerator SpawnObj(GameObject obj, Transform tar, float delay)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(obj, tar.position, tar.rotation);
    }
}
