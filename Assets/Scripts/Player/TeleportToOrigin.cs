using UnityEngine;

public class TeleportToOrigin : MonoBehaviour
{

    public float xCoordinate;
    public float yCoordinate;
    public float zCoordinate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Abyss"))
        {
            transform.position = new Vector3(xCoordinate, yCoordinate, zCoordinate);
        }
    }
}
