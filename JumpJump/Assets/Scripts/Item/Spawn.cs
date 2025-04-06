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
        GameObject obj = (GameObject)parent;
        GameObject newCoin = Instantiate(coinPrefabs, obj.transform.GetChild(0).transform.position, Quaternion.identity, obj.transform.GetChild(0).transform);
    }

    public void SpawnDiamond(object parent)
    {
        GameObject obj = (GameObject)parent;
        GameObject newDiamond = Instantiate(diamondPrefabs, obj.transform.GetChild(0).transform.position, Quaternion.identity, obj.transform.GetChild(0).transform);
    }
}

