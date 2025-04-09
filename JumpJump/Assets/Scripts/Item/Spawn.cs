using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefabs;
    [SerializeField] private GameObject diamondPrefabs;
    private void Awake()
    {
        ObserverManager<IDGameEven>.AddDesgisterEvent(IDGameEven.SpawnCoin, SpawnCoin);
        ObserverManager<IDGameEven>.AddDesgisterEvent(IDGameEven.SpawnDiamond, SpawnDiamond);
    }
    public void SpawnCoin(object parent)
    {
        SpawnItem(coinPrefabs, parent);
    }

    public void SpawnDiamond(object parent)
    {
        SpawnItem(diamondPrefabs, parent);
    }

    private void SpawnItem(GameObject prefab, object parent)
    {
        if (parent is not GameObject obj || obj.transform.childCount == 0) return;

        Transform spawnPoint = obj.transform.GetChild(0);
        Instantiate(prefab, spawnPoint.position, Quaternion.identity, spawnPoint);
    }
}

