using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemiesMovement : MonoBehaviour
{
    public Transform[] waypoints;
    public Transform santaMorpho; 
    public GameObject feedBackNegative;
    public AudioSource audioSource;
    public float speed;
    private int currentWaypointIndex = 0;
    private float distanceMin = 0.05f;

    public bool isChasing = false;
    public bool isReturningToPatrol = false;
    private Vector3 lastPosition; 
    public string movingDirection;
    
    private Vector3 lastPositionTarget; 
    public Animator animator;
    

    void start()
    {
        animator = GetComponent<Animator>(); 
    }

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
        speed=0.5f;

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
        if(Vector2.Distance(transform.position, santaMorpho.position)< 1.0f)
        {
            animator.SetBool("Idle", true);
            animator.SetBool("goRight", false);
            animator.SetBool("goLeft", false);
            animator.SetBool("goUp", false);
            animator.SetBool("GoDown", false);
            isChasing = false;
            Time.timeScale = 0;
            feedBackNegative.SetActive(true);
            audioSource.Stop();
        }
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
        
        Vector3 currentPosition = transform.position;
        Vector3 movement = currentPosition - lastPosition;

        
        if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
        {
            if (movement.x > 0)
            {
                movingDirection = "right";
                animator.SetBool("goRight", true);
                animator.SetBool("goLeft", false);
                animator.SetBool("goUp", false);
                animator.SetBool("GoDown", false);


                transform.localScale = new Vector3(1, 1, 1);

            }
                
            else if (movement.x < 0)
            {
                movingDirection = "left";
                animator.SetBool("goLeft", true);
                animator.SetBool("goRight", false);
                animator.SetBool("goUp", false);
                animator.SetBool("GoDown", false);
                transform.localScale = new Vector3(-1, 1, 1);
                
            }
                
        }
        else
        {
            if (movement.y > 0)
            {
                movingDirection = "up";
                
                
                    animator.SetBool("goUp", true);
                    animator.SetBool("GoDown", false);
                    animator.SetBool("goLeft", false);
                    animator.SetBool("goRight", false);
                
            }
                
            else if (movement.y < 0)
            {
                movingDirection = "down";
                animator.SetBool("GoDown", true);
                animator.SetBool("goUp", false);
                animator.SetBool("goLeft", false);
                animator.SetBool("goRight", false);

            }
            
        }

       
        lastPosition = currentPosition;
    }

}
