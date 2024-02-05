using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class playerControls : MonoBehaviour
{
    float characterXPos;
    public GameObject player;
    BallController ballController;
    void Start()
    {
        
        //characterXPos =transform.position.x;
        ballController=FindAnyObjectByType<BallController>();
        //player.transform.DOMoveX(ballController.targetxPos,0.3f);
       
    }
    public void moveTheFCharacter(){
        player.transform.DOMoveX(ballController.targetxPos,0.3f);
    }
    void Update()
    {
        
    }
}
