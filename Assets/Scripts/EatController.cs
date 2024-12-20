using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatController : MonoBehaviour
{
    public int points=0;
    public bool isEating;

    [SerializeField] private GameObject foodParticle;

    [SerializeField] private float eatingTime;
    
    private void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Food") && isEating)
        {
            StartCoroutine(EatCookie());
            other.gameObject.SetActive(false);
            points++;
        }
    }

    private IEnumerator EatCookie()
    {
        foodParticle.SetActive(true);
        isEating  = false;
        yield return new WaitForSeconds(eatingTime);

        foodParticle.SetActive(false);
    }
}
