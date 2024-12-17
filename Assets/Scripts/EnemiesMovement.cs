using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesMovement : MonoBehaviour
{
    public Transform[] waypoints;

    public float speed = 2f;

    private int currentWaypointIndex = 0;

    private float distanceMin = 0.1f;

    void Update()
    {
        movementEnemy();
    }

    private void movementEnemy()

    {
        if (waypoints.Length == 0) return;

     
        Transform targetWaypoint = waypoints[currentWaypointIndex];

        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetWaypoint.position) < distanceMin)
        {
            currentWaypointIndex++;

            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
    }
}





