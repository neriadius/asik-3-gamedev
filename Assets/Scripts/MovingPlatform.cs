using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float speed = 2.0f;
    private int currentWaypointIndex = 0;

    void FixedUpdate()
    {
        if (waypoints.Length == 0) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            waypoints[currentWaypointIndex].position,
            speed * Time.fixedDeltaTime
        );

        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
}
