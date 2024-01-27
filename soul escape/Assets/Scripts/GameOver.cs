using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
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
        BallController ballController = FindAnyObjectByType<BallController>();
        
       
       if (gridManager!=null)
       {
        gridManager.gameObject.SetActive(false);
       } 
       if (ballController!=null)
       {
        ballController.gameObject.SetActive(false);
       }
    }
}
