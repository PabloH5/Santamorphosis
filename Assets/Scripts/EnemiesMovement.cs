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
    private Vector3 lastPosition; // Almacena la última posición del enemigo
    public string movingDirection;
    public Transform rangoVisionMesh;
    


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

        directionMovement();
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


   private void directionMovement()

{
    // Calcula la diferencia de posición
    Vector3 currentPosition = transform.position;
    Vector3 movement = currentPosition - lastPosition;

    // Determinar la dirección según el movimiento
    if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
    {
        if (movement.x > 0)
        {
            movingDirection = "right"; // Se mueve hacia la derecha
            rangoVisionMesh.rotation = Quaternion.Euler(0f, 0f, 90.0f);

        }
            
        else if (movement.x < 0)
        {
            movingDirection = "left"; // Se mueve hacia la izquierda
            rangoVisionMesh.rotation = Quaternion.Euler(0f, 0f, 270.0f);
        }
            
    }
    else
    {
        if (movement.y > 0)
        {
            movingDirection = "up"; // Se mueve hacia arriba
            rangoVisionMesh.rotation = Quaternion.Euler(0f, 0f, 180.0f);
        }
            
        else if (movement.y < 0)
            movingDirection = "down"; // Se mueve hacia abajo
    }

    // Guardar la posición actual para usarla como la previa en el siguiente frame
    lastPosition = currentPosition;

    // Ejemplo: Imprime la dirección en la consola
    Debug.Log("Enemy is moving: " + movingDirection);
}

}
