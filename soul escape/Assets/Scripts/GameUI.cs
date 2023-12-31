using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision) {
        
        if(collision.gameObject.CompareTag("Tile"))
        {
            endGame();
        }
    }

    private void endGame()
    {
        Debug.Log("GAME OVER!");
        //Time.timeScale=0f;
        GridManager gridManager = FindObjectOfType<GridManager>();
        BallManager ballManager = FindAnyObjectByType<BallManager>();
       
       if (gridManager!=null)
       {
        gridManager.gameObject.SetActive(false);
       } 
       if (ballManager!=null)
       {
        ballManager.gameObject.SetActive(false);
       }
    }
}
