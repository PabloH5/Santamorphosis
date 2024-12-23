using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Controllers")]
    [SerializeField] private GameObject player;


    [Header("UI elements")]
    [SerializeField] private Text pointsText;

    [SerializeField] private Image dashTimer;


    [Header("Win Conditions")]
    [SerializeField] private GameObject winZone;
    [SerializeField] private int pointsToWin;

    private MovementController movementController;
    private EatController eatController;

    // Start is called before the first frame update
    void Start()
    {
        eatController = player.GetComponent<EatController>();
        movementController = player.GetComponent<MovementController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        pointsText.text = "x " + eatController.points;
        UpdateDashFill();
        ActivateWinZone(eatController.points);
    }

    private void UpdateDashFill()
    {
        // Here we map the timer to a 0-1 range for the fill amount
        float fillValue = movementController.dashTimer / movementController.dashCoolDown;
        dashTimer.fillAmount = Mathf.Clamp01(fillValue);
    }

    private void ActivateWinZone(int point)
    {
        if (point >= pointsToWin)
        {
            winZone.SetActive(true);
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Play()
    {
        Time.timeScale = 1;
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

     public void LoadScene(String sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
