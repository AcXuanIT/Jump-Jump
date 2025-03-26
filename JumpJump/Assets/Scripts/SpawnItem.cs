using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    [Space]
    [Header("Object")]
    [SerializeField] private List<GameObject> objectPrefabs;
    [SerializeField] private Transform spawnObject;
    [SerializeField] private List<Vector3> listPositionObjectStart;

    [Header("Time")]
    private float timeSpawn = 0f;
    [SerializeField] private float timeDelay;

    [Space]
    [Header("Random")]
    [SerializeField] private float minXrandom;
    [SerializeField] private float maxXrandom;
    [SerializeField] private float Yconst;

    private void Awake()
    {
        this.SpawnStart();
    }
    private void Update()
    {
        Spawn();
    }
    public void SpawnStart()
    {
        for (int i = 0; i < listPositionObjectStart.Count; i++)
        {
            PoolingManager.Spawn(objectPrefabs[RandomPrefabs()], listPositionObjectStart[i], Quaternion.identity, spawnObject);
        }
    }
    public Vector3 RandomPosition()
    {
        float randomX = Random.Range(minXrandom, maxXrandom);
        return new Vector3(randomX, Yconst, 0);
    }
    public int RandomPrefabs()
    {
        return Random.Range(0, objectPrefabs.Count);
    }
    public void Spawn()
    {
        timeSpawn += Time.deltaTime;
        if (timeSpawn < timeDelay) return;
        timeSpawn = 0;

        PoolingManager.Spawn(objectPrefabs[RandomPrefabs()], RandomPosition(), Quaternion.identity, spawnObject);
    }
}
