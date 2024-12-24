using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeVIsionShort : MonoBehaviour
{
    public EnemiesMovement enemiesMovement;
    public RangeVision rangeVision;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            
            enemiesMovement.isChasing = true;
            enemiesMovement.isReturningToPatrol = false;
            enemiesMovement.speed = 1f; 
            StartCoroutine(rangeVision.CheckLastKnownPosition());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
