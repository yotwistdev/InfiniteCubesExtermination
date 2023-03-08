using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public LayerMask clickLayer;

    public static event Action<Vector3, Vector3> OnClick;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Click();
        }
    }

    private void Click()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        Vector3 cameraPoint = ray.GetPoint(1f);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f, clickLayer))
        {
            Vector3 objectPoint = hit.point;
            GameObject objectClicked = hit.collider.gameObject;

            OnClick?.Invoke(cameraPoint, objectPoint);
        }
    }
}