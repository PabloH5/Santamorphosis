using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinController : MonoBehaviour
{
    [SerializeField] private GameObject winFeedback;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            winFeedback.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
