using UnityEngine;

public class MovableObject : MonoBehaviour
{
    public float speed = 5f;

    public float xCoordinate;
    public float yCoordinate;
    public float zCoordinate;

    public float maxXCoordinate;
    public float maxYCoordinate;
    public float maxZCoordinate;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private Vector3 moveDirection;

    private bool movingForward = true;

    private void Awake()
    {
        startPosition = transform.position;
        targetPosition = new Vector3(maxXCoordinate, maxYCoordinate, maxZCoordinate);

        moveDirection = new Vector3(xCoordinate, yCoordinate, zCoordinate).normalized;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (movingForward)
        {
            transform.Translate(moveDirection * speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                transform.position = targetPosition;
                movingForward = false;
            }
        }
        else
        {
            transform.Translate(-moveDirection * speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, startPosition) < 0.1f)
            {
                transform.position = startPosition;
                movingForward = true;
            }
        }
    }
}