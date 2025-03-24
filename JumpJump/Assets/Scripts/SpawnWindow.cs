using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWindow : MonoBehaviour
{
    [SerializeField] private GameObject plankPrefab;
    [SerializeField] private Transform spawnPlank;
    [SerializeField] private List<Vector3> listPositionPlankStart;

    [Header("Time")]
    private float timeSpawn = 0f;
    [SerializeField] private float timeDelay;

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
        for (int i = 0; i < listPositionPlankStart.Count; i++)
        {
            PoolingManager.Spawn(plankPrefab, listPositionPlankStart[i], Quaternion.identity, spawnPlank);
        }
    }
    public Vector3 RandomPosition()
    {
        float randomX = Random.Range(-1.5f, 1.5f);
        float randomY = 6f;

        return new Vector3(randomX, randomY, 0);
    }
    public void Spawn()
    {
        timeSpawn += Time.deltaTime;
        if (timeSpawn < timeDelay) return;
        timeSpawn = 0;

        PoolingManager.Spawn(plankPrefab, RandomPosition(), Quaternion.identity, spawnPlank);
    }
}
