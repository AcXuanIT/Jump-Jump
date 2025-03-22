using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    private bool dontDestroyOnLoad;

    public static T Instance
    {
        get
        {
            if(instance == null) // kiem tra xem instance co null hay khong
            {
                instance = FindObjectOfType<T>(true);
                if(instance == null) // kiem tra xem instance co null nua hay khong 
                {
                    GameObject singleton = new GameObject(typeof(T).Name); // tao gameobject moi co ten giong voi kieu T
                    instance = singleton.AddComponent<T>(); // gan instance = singleton , them component T vao instance
                }
            }
            return instance;
        }
    }

    protected virtual void KeepActive(bool isOnLoad) // singleton co nen giu lai khi load scene khong
    {
        this.dontDestroyOnLoad = isOnLoad;
    }

    protected virtual void Awake()
    {
        //kiem tra xem instance khong phai this => xoa gameobject
        if (instance != null && instance.GetInstanceID() != this.GetInstanceID()) 
        {
            Destroy(gameObject);
            return;
        }

        instance = this as T;

        if(dontDestroyOnLoad)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
