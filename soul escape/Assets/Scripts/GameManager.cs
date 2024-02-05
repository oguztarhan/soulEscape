using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>(true);
            }

            return instance;
        }
    }
    public int hasreturn;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
    }

    public void AddHasReturn()
    {
        hasreturn++;

        if (hasreturn+1 == BallController.Instance.ballCount)
        {
            BallController.Instance.ChangeBallState(BallController.ballState.endShot);
            ResetHasReturn();

        }

    }

    public void ResetHasReturn()
    {
        hasreturn = 0;
    }

}