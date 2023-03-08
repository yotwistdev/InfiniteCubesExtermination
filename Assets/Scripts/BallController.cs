using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour
{
    public float speed = 50f;
    public float arcHeight = 5f;
    public float explosionRadius = 1.5f;
    public LayerMask cubeLayer;
    private Vector3 goalPosition;

    public void SetTarget(Vector3 _goalPosition)
    {
        goalPosition = _goalPosition;
        StartCoroutine(Throw());
    }

    private IEnumerator Throw()
    {
        Vector3 start = transform.position;
        float distance = Vector3.Distance(start, goalPosition);
        float distCovered = 0f;
        float distFraction = 0f;

        while (distFraction < 1f)
        {
            distCovered += speed * Time.deltaTime;
            distFraction = distCovered / distance;

            float heightOffset = arcHeight * Mathf.Sin(distFraction * Mathf.PI);
            transform.position = Vector3.Lerp(start, goalPosition, distFraction) + Vector3.up * heightOffset;

            yield return null;
        }

        Explode();
        yield return null;
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, cubeLayer, QueryTriggerInteraction.Collide);

        foreach (Collider collider in colliders)
        {
            collider.GetComponent<CubeController>().Exterminate();
        }

        BallCannon.Despawn(gameObject);
    }
}
