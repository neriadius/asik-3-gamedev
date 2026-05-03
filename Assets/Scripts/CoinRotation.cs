using UnityEngine;

public class CoinRotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private Vector3 rotationAxis = Vector3.up;

    void Update()
    {
        // Multiplied by Time.deltaTime to ensure consistent speed across different frame rates
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }
}