using UnityEngine;

public class RotateAroundReference : MonoBehaviour
{
    [SerializeField] private Transform referencePoint; // The point to rotate around
    [SerializeField] private float rotationSpeed = 10f; // Speed of rotation in degrees per second

    private void Update()
    {
        if (referencePoint == null)
        {
            Debug.LogWarning("Reference point not set!");
            return;
        }

        // Rotate around the reference point on the Y-axis
        transform.RotateAround(referencePoint.position, Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
