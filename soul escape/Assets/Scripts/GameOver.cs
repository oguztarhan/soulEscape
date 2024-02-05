using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverPanel;
    [SerializeField] TextMeshProUGUI scorre;
    GridManager gridManager;
    private void OnCollisionEnter(Collision collision) {
        
        if(collision.gameObject.CompareTag("Tile"))
        {
            endGame();
        }
        
    }
    private void Start() {
        gameOverPanel.SetActive(false);
    }
    private void endGame()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale=0f;
        gridManager = FindObjectOfType<GridManager>();
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
     
    public void restartTheGame(){
        SceneManager.LoadScene(1);
            Time.timeScale = 1f;
    }
}
