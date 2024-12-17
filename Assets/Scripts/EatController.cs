using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatController : MonoBehaviour
{
    public int points=0;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Food"))
        {
            other.gameObject.SetActive(false);
            points++;
        }
    }
}
