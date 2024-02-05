using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausep;
    public GameObject scorePanel;
    
    
    void Start()
    {
        pausep.SetActive(false);

    }
    public void pauseTheGame()
    {
        pausep.SetActive(true);
        scorePanel.SetActive(false);
        Time.timeScale = 0f;
        MusicPlayer.Instance.PauseMusic();
    }
    public void resumeTheGame()
    {
        pausep.SetActive(false);
        scorePanel.SetActive(true);
        Time.timeScale = 1f;
        MusicPlayer.Instance.ResumeMusic();
    }
    public void restartTheGame()
    {
        MusicPlayer.Instance.RestartMusic();
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }
    public void quitTheGame()
    {
        MusicPlayer.Instance.PauseMusic();
        SceneManager.LoadScene(0);
    }

    // [System.Obsolete]
    /*public void butondan_gelen(string ne_geldi)
    {
        if (ne_geldi == "P")
        { 
            Debug.Log("girdi");
            pausep.SetActive (true);
            background.SetActive(true);
           // scorePanel.SetActive(false);
            Time.timeScale = 0f;
        }else if(ne_geldi == "devamet")
        {
            pausep.SetActive (false);
            background.SetActive(false);
           // scorePanel.SetActive(true);
            Time.timeScale = 1f;
        }else if (ne_geldi == "yeniden")    
        {
            Application.LoadLevel(1);
            Time.timeScale = 1f;
        }
        else if (ne_geldi == "AnaMenu")
        {
            SceneManager.LoadScene(0);
        }
    }*/
}
