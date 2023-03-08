using UnityEngine;
using System.Collections;

public class CubeController : MonoBehaviour
{
    public float speed = 1f;
    public float rotationSpeed = 1f;
    public float dashSpeed = 5f;
    public float dashDuration = 0.1f;
    public float dashIntervalMin = 3f;
    public float dashIntervalMax = 6f;

    private Vector3 goalPosition;
    private bool isDashing;
    private float nextDashTime;

    private void Start () 
    {
        goalPosition = GetRandomPosition();
        isDashing = false;
        nextDashTime = Time.time + Random.Range(dashIntervalMin, dashIntervalMax);
    }

    private void Update () 
    {
        if (transform.position == goalPosition)
        {
            goalPosition = GetRandomPosition();
        }

        MoveAndRotate();

        if (!isDashing && Time.time > nextDashTime) 
        {
            StartCoroutine(Dash());
            nextDashTime = Time.time + Random.Range(dashIntervalMin, dashIntervalMax);
        } 
    }

    private void MoveAndRotate () 
    {
        transform.position = Vector3.MoveTowards(transform.position, goalPosition, speed * Time.deltaTime);

        Vector3 targetDirection = goalPosition - transform.position;
        if (targetDirection != Vector3.zero) 
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private IEnumerator Dash() 
    {
        isDashing = true;

        Vector3 dashDirection = goalPosition - transform.position;
        float dashDistance = Mathf.Min(dashSpeed * dashDuration, dashDirection.magnitude);
        dashDirection.Normalize();

        float dashEndTime = Time.time + dashDuration;
        while (Time.time < dashEndTime) 
        {
            transform.position += dashDirection * dashSpeed * Time.deltaTime;
            yield return null;
        }

        isDashing = false;
    }

    private Vector3 GetRandomPosition() 
    {
        Vector3 size = new Vector3(8, 0, 8);
        Vector3 randomPosition = new Vector3(Random.Range(-size.x / 2, size.x / 2), 0, Random.Range(-size.z / 2, size.z / 2));
        return randomPosition;
    }

    public void Exterminate()
    {
        CubeSpawner.OnCubeExterminated(gameObject);
    }
}
