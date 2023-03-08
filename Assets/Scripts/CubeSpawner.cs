using UnityEngine;
using System;

[RequireComponent(typeof(ObjectPool))]
public class CubeSpawner : MonoBehaviour
{
    public GameObject cubePrefab;
    public int maxCubeCount = 3;

    public static event Action<int> OnFragCountChange;

    private static ObjectPool cubePool;
    private static int fragCount = 0;

    private void Start()
    {
        cubePool = GetComponent<ObjectPool>();
        cubePool.InitializePool(3, cubePrefab);
        for (int i = 0; i < maxCubeCount; i++)
        {
            Spawn();
        }
    }

    private static void Spawn()
    {
        cubePool.Spawn(Vector3.zero);
    }

    public static void OnCubeExterminated(GameObject obj)
    {
        cubePool.Despawn(obj);
        fragCount++;
        OnFragCountChange?.Invoke(fragCount);
        Spawn();
    }
}
