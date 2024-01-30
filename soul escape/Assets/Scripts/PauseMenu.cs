using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausep;
    void Start()
    {
        pausep.SetActive(false);
    }

    
    void Update()
    {
        
    }
    public void butondan_gelen(string ne_geldi)
    {
        if (ne_geldi == "pause")
        {
            pausep.SetActive (true);
            Time.timeScale = 0f;
        }else if(ne_geldi == "devamet")
        {
            pausep.SetActive (false);
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
    }
}
