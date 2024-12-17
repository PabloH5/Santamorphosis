using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesMovement : MonoBehaviour
{
    public Transform[] waypoints;

    public Transform santaMorpho;

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

        transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetWaypoint.position) < distanceMin)
        {
            currentWaypointIndex++;

            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }

        if(Vector2.Distance(transform.position, santaMorpho.position) < 2.0f)
        {
            transform.position = Vector2.MoveTowards(transform.position, santaMorpho.position, speed * Time.deltaTime);
        }
    }
}





