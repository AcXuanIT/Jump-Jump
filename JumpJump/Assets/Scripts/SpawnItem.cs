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
    [SerializeField] private int randomNext;

    private void Awake()
    {
        this.randomNext = Random.Range(6, 11);
        this.SpawnStart();
        ObserverManager<IDGameEven>.AddDesgisterEvent(IDGameEven.TimeDelay, UpTimeDelay);
    }
    private void Update()
    {
        Spawn();
    }
    public void SpawnStart()
    {
        for (int i = 0; i < listPositionObjectStart.Count; i++)
        {
            GameObject newObj = PoolingManager.Spawn(objectPrefabs[0], listPositionObjectStart[i], Quaternion.identity, spawnObject);
            checkPlank(newObj);
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

        GameObject newobj = PoolingManager.Spawn(objectPrefabs[RandomPrefabs()], RandomPosition(), Quaternion.identity, spawnObject);
        checkPlank(newobj);
    }

    public void UpTimeDelay(object obj)
    {
        this.timeDelay /= (float)obj;
    }

    public void checkPlank(GameObject obj)
    {
        if(obj.CompareTag("Plank"))
        {
            GameController.Instance.Planks++;

            if(GameController.Instance.Planks >= GameController.Instance.PlanksLate + randomNext)
            {
                ObserverManager<IDGameEven>.PostEven(IDGameEven.SpawnCoin, obj);
                GameController.Instance.Coins++;
                GameController.Instance.PlanksLate += randomNext;
                this.randomNext = Random.Range(6, 11);
            }
            else if(GameController.Instance.Coins >= GameController.Instance.CoinsLate + 5)
            {
                GameController.Instance.CoinsLate += 5;
                ObserverManager<IDGameEven>.PostEven(IDGameEven.SpawnDiamond, obj);
            }
        }    
    }
}
