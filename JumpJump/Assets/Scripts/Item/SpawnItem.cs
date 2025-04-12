using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    [Space]
    [Header("Object")]
    [SerializeField] private List<GameObject> objectPrefabs;
    [SerializeField] private Transform spawnObject;
    [SerializeField] private GameObject plankOld;
    [SerializeField] private List<Vector3> listPositionObjectStart;

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
    }
    private void Update()
    {
        if (GameController.Instance.Mode != ModeGame.Play) return;

        Spawn();
    }
    public void SpawnStart()
    {
        for (int i = 0; i < listPositionObjectStart.Count; i++)
        {
            GameObject newObj = PoolingManager.Spawn(objectPrefabs[0], listPositionObjectStart[i], Quaternion.identity, spawnObject);
            this.plankOld = newObj;
            checkPlank(newObj);
        }
    }
    public Vector3 RandomPosition()
    {
        float randomX = Random.Range(minXrandom, maxXrandom);
        float value = Random.Range(0, 2) == 0 ? 6 : 8;
        return new Vector3(randomX, value, 0);
    }
    public int RandomPrefabs()
    {
        return Random.Range(0, objectPrefabs.Count);
    }
    public void Spawn()
    {
        if (GameController.Instance.Mode != ModeGame.Play) return;

        if (plankOld.transform.position.y > 4) return;

        GameObject newobj = PoolingManager.Spawn(objectPrefabs[RandomPrefabs()], RandomPosition(), Quaternion.identity, spawnObject);
        this.plankOld = newobj;
        checkPlank(newobj);
    }
    public void checkPlank(GameObject obj)
    {
        if(obj.TryGetComponent<Plank>(out var plank))
        {
            GameController.Instance.Planks++;

            if(GameController.Instance.Planks >= GameController.Instance.PlanksLate + randomNext)
            {
                ObserverManager<IDGameEven>.PostEven(IDGameEven.SpawnCoin, plank.gameObject);
                GameController.Instance.Coins++;
                GameController.Instance.PlanksLate += randomNext;
                this.randomNext = Random.Range(4, 8);
                return;
            }
            else if(GameController.Instance.Coins >= GameController.Instance.CoinsLate + 5)
            {
                GameController.Instance.CoinsLate += 5;
                ObserverManager<IDGameEven>.PostEven(IDGameEven.SpawnDiamond, plank.gameObject);
            }
            else
            {
                this.SpawnRandomItem(plank.gameObject);
            }
        }    
    }

    public void SpawnRandomItem(object plank)
    {
        this.SpawnBomb(plank);
    }
    public void SpawnBomb(object plank)
    {
        float value = Random.value;
        if(value <= 0.2f)
        {
            ObserverManager<IDGameEven>.PostEven(IDGameEven.SpawnBomb, plank);
        }
    }
}
