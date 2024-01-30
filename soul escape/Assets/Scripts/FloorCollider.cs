using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Xml;

public class FloorCollider : MonoBehaviour
{
    private int destroyingBalls;
    public int results;
    BallController ballController;
    
    public void Start()
    {
        ballController = FindAnyObjectByType<BallController>(); 
    }


    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag=="ExtraBall")
        {
            destroyingBalls--;
            Destroy(collision.gameObject);
            //collision.gameObject.GetComponent<Rigidbody>().velocity=Vector3.zero;
            
            
        }
        
            if(destroyingBalls<=1){
            ballController.currentBallState =  BallController.ballState.aim;
        }
    }
}
