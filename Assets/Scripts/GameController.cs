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

    [SerializeField] private Slider transformCdBar;

    [SerializeField] private GameObject endText;


    [Header("Win Conditions")]
    [SerializeField] private GameObject winZone;
    [SerializeField] private int pointsToWin;

    private MovementController _movementController;
    private EatController _eatController;
    private MetamorphosisController _metamorphosisController;

    void Start()
    {
        _eatController = player.GetComponent<EatController>();
        _movementController = player.GetComponent<MovementController>();
        _metamorphosisController = player.GetComponent<MetamorphosisController>();
    }

    void FixedUpdate()
    {
        pointsText.text = "x " + _eatController.points;
        UpdateDashFill();
        MetamorphisisCD();
        ActivateWinZone(_eatController.points);
    }

    private void UpdateDashFill()
    {
        if (_movementController.dashTimer <= 0)
        {
            dashTimer.gameObject.SetActive(false);
        }
        else
        {
            // Here we map the timer to a 0-1 range for the fill amount
            dashTimer.gameObject.SetActive(true);
            float fillValue = _movementController.dashTimer / _movementController.dashCoolDown;
            dashTimer.fillAmount = Mathf.Clamp01(fillValue);
        }
    }

    private void MetamorphisisCD()
    {
        if(_metamorphosisController.transformTimer <=0)
        {
            transformCdBar.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            transformCdBar.transform.parent.gameObject.SetActive(true);
            float fillValue = _metamorphosisController.transformTimer / _metamorphosisController.transformCoolDown;
            transformCdBar.value = Mathf.Clamp01(fillValue);
        }
    }

    private void ActivateWinZone(int point)
    {
        if (point >= pointsToWin)
        {
            StartCoroutine(TurnOffObj(endText, 5f));
            winZone.SetActive(true);
        }
    }

    private IEnumerator TurnOffObj(GameObject gameObject, float seconds)
    {
        gameObject.SetActive(true);
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(false);
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
