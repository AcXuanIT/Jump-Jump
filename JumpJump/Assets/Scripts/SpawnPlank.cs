using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlank : MonoBehaviour
{
    [SerializeField] private GameObject plankPrefab;

    [Header("Time")]
    private float timeSpawn = 0f;
    private float timeDelay = 5f;

    private void Update()
    {
        Spawn();
    }
    public void Spawn()
    {
        timeSpawn += Time.deltaTime;
        if (timeSpawn < timeDelay) return;
        timeSpawn = 0;

        PoolingManager.Spawn(plankPrefab);
    }
}
