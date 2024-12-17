using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Text pointsText;

    [SerializeField] private Image dashTimer;
    private float dashCD;

    [SerializeField] private GameObject player;

    private MovementController movementController;
    private EatController eatController;
    
    // Start is called before the first frame update
    void Start()
    {
        eatController =  player.GetComponent<EatController>();
        movementController = player.GetComponent<MovementController>();

        dashCD = movementController.dashCoolDown;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        pointsText.text = "X " + eatController.points;
        UpdateDashFill();

    }

    private void UpdateDashFill()
    {

        // Here we map the timer to a 0-1 range for the fill amount
        float fillValue = movementController.dashTimer / movementController.dashCoolDown;
        dashTimer.fillAmount = Mathf.Clamp01(fillValue);
    }
}
