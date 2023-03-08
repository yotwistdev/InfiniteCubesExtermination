using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    private GameObject prefab;
    private List<GameObject> objectPool = new List<GameObject>();

    public void InitializePool(int poolSize, GameObject _prefab)
    {
        prefab = _prefab;
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            objectPool.Add(obj);
        }
    }

    public GameObject Spawn(Vector3 spawnPosition)
    {
        GameObject obj = objectPool.Find(o => !o.activeSelf);

        if (obj == null)
        {
            obj = Instantiate(prefab);
            objectPool.Add(obj);
        }

        obj.SetActive(true);
        obj.transform.position = spawnPosition;
        return obj;
    }

    public void Despawn(GameObject obj)
    {
        obj.SetActive(false);
        objectPool.Add(obj);
    }
}
