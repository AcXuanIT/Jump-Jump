using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private Transform objectSpawn;
    [SerializeField] private GameObject coinPrefabs;
    [SerializeField] private GameObject diamondPrefabs;


    public void SpawnObject(GameObject obj, Vector3 pos)
    {
        GameObject newObj = PoolingManager.Spawn(obj, pos, Quaternion.identity);
        newObj.transform.SetParent(objectSpawn);
    }
}
