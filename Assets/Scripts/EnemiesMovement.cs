using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemiesMovement : MonoBehaviour
{
    public Transform[] waypoints;
    public Transform santaMorpho; 

    public float speed = 2f;
    private int currentWaypointIndex = 0;
    private float distanceMin = 0.05f;

    public bool isChasing = false;
    public bool isReturningToPatrol = false;

    private Vector3 lastPositionTarget; 

    void Update()
    {
        if (isChasing)
        {
            chasingPlayer();
        }
        else if (isReturningToPatrol)
        {
            movementEnemy(); 
        }
        else
        {
            movementEnemy(); 
        }
    }

    private void movementEnemy()
    {
        if (waypoints.Length == 0) return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetWaypoint.position) < distanceMin)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length) currentWaypointIndex = 0;
        }
    }

    public void chasingPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, santaMorpho.position, speed * Time.deltaTime);
    }

    public void MoveToLastPosition(Vector3 position)
    {
        lastPositionTarget = position;
        isReturningToPatrol = false; 
    }

    public bool ReachedLastPosition()
    {
        
        transform.position = Vector2.MoveTowards(transform.position, lastPositionTarget, speed * Time.deltaTime);

        
        return Vector2.Distance(transform.position, lastPositionTarget) > distanceMin;
    }
}
