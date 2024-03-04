using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    private const float MinDistance = 0.1f;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float movingSpeed = 2f;

    private int _waypointNumber;

    private void Update()
    {
        MovePlatform();
    }

    private void MovePlatform()
    {
        if (waypoints.Length == 0) Debug.LogWarning("No Waypoints Assigned for this Platform");

        var currentWaypoint = waypoints[_waypointNumber];

        Vector2 currentPosition = transform.position;
        Vector2 waypointPosition = currentWaypoint.position;
        var distance = currentPosition - waypointPosition;
        distance.Normalize();

        currentPosition = Vector2.MoveTowards(currentPosition, waypointPosition,
            movingSpeed * Time.deltaTime);
        transform.position = currentPosition;
        if (Vector2.Distance(currentPosition, waypointPosition) < MinDistance) _waypointNumber++;
        if (_waypointNumber >= waypoints.Length) _waypointNumber = 0;
    }
}