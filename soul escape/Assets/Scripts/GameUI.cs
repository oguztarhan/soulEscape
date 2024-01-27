using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{   
    public void startGame(){
        SceneManager.LoadScene(1);
    }
    public void pauseGame(){
        //gameObject.SetActive(true);
    }
}
