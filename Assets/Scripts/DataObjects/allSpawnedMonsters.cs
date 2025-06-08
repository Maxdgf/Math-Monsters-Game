using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class allSpawnedMonsters : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> spawnedMonsters = new List<GameObject>();

    public void addMonsterToStorage(GameObject monster)
    {
        spawnedMonsters.Add(monster);
    }

    public void removeMonsterWithIndexInStorage(int index)
    {
        spawnedMonsters.RemoveAt(index);
    }

    public GameObject getMonsterFromIndex(int index)
    {
        return spawnedMonsters[index];
    }

    public int returnMonstersStorageLength()
    {
        return spawnedMonsters.Count;
    }
}