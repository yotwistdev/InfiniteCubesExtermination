using UnityEngine;
using System;

[RequireComponent(typeof(ObjectPool))]
public class BallCannon : MonoBehaviour
{
    public GameObject ballPrefab;
    public int maxBallCount = 5;
    public float restoreInterval = 1f;
    public float cooldownInterval = 0.5f;

    public static event Action<int> OnBallChange;
    public static event Action<float> OnRestoreTimeChange;

    private static ObjectPool ballPool;
    private int ballCount = 5;
    private float restoreTime = 1f;
    private float cooldownTime = 0f;
    private bool onCooldown = false;

    private void Start()
    {
        ballPool = GetComponent<ObjectPool>();
        ballPool.InitializePool(5, ballPrefab);
        PlayerController.OnClick += ShootBall;
        restoreTime = restoreInterval;
    }

    private void Update()
    {
        if (ballCount < maxBallCount)
        {
            restoreTime -= Time.deltaTime;
            OnRestoreTimeChange?.Invoke(restoreTime);
            if (restoreTime <= 0f)
            {
                ballCount++;
                restoreTime = restoreInterval;
                OnBallChange?.Invoke(ballCount);
            }
        }

        if (onCooldown)
        {
            cooldownTime -= Time.deltaTime;
            if (cooldownTime <= 0f)
            {
                onCooldown = false;
                cooldownTime = 0f;
            }
        }
    }

    private void ShootBall(Vector3 origin, Vector3 goalPosition)
    {
        if (ballCount <= 0 || onCooldown)
        {
            return;
        }

        GameObject ball = ballPool.Spawn(origin);
        ball.GetComponent<BallController>().SetTarget(goalPosition);

        ballCount--;
        OnBallChange?.Invoke(ballCount);

        onCooldown = true;
        cooldownTime = cooldownInterval;
    }

    public static void Despawn(GameObject obj)
    {
        ballPool.Despawn(obj);
    }
}
