using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class GameUI : MonoBehaviour
{
    GridManager gridManager;
    BallController ballController;
    [SerializeField] TextMeshProUGUI Level, ballCount,scorre;
    int GetLevel, GetBallCount;
    public Sprite soundOn;
    public Sprite soundOff;
    private bool isOn = true;
    public Button button;
    public GameObject ball;
    Vector3 lastPos;

    Vector3 lastVelocity;

    [SerializeField] Rigidbody top;

    public void startGame()
    {

        SceneManager.LoadScene(1);

        // soundOn=button.image.sprite;
    }

    private void Start()
    {
        
        gridManager = FindAnyObjectByType<GridManager>();
        ballController = FindAnyObjectByType<BallController>();
        lastPos=ball.transform.position;
        

    }
    private void Update()
    {
        GetLevel = gridManager.level;
        GetBallCount = ballController.ballCount;
        Level.text = "" + GetLevel;
        ballCount.text = "x" + GetBallCount;
        scorre.text=""+GetLevel;
    }
    public void ResetTheBalls()
    {
        ball.transform.position=ballController.instantiatePosition;
        top.velocity = new Vector3(0f, 0f, 0f);
        
        GameObject[] extraBalls = GameObject.FindGameObjectsWithTag("ExtraBall");
        foreach (GameObject extraBall in extraBalls)
        {
            Destroy(extraBall);
            GameManager.Instance.AddHasReturn();
        }

    }
    public void MuteSound()
    {
        if (isOn)
        {
            button.image.sprite = soundOff;
            isOn = false;
            AudioListener.volume = 0;
        }
        else
        {
            button.image.sprite = soundOn;
            isOn = true;
            AudioListener.volume = 1;
        }
    }




}
