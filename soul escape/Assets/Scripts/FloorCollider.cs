using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Xml;
using UnityEditor;

public class FloorCollider : MonoBehaviour
{
    private int destroyingBalls;
    // [SerializeField] Rigidbody ball;
   public int ballCount=1;
   public int plusCount;
   public int hasReturn;
    BallController ballController;
    GridManager gridManager;
    public void Start()
    {
        ballController = FindAnyObjectByType<BallController>(); 
        gridManager = FindObjectOfType<GridManager>();
        
         
    }
   public void Update(){
   // Debug.Log("tümtoplar:"+ballCount);
   // Debug.Log("+toplar:"+plusCount);
    
   }
    
   
    /* void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Ball"))
        {
            //ballController = collision.gameObject.GetComponent<BallController>();
            ballController.ball.velocity = new Vector3(0f,0f,0f);
            ballController.ball.angularVelocity = Vector3.zero;
            ballController.ballPos=transform.position;
            ballController.ballRot=transform.rotation;
            hasReturn++;
            Debug.Log(hasReturn);
        }
    }*/
     public void WaitForTheShot(){
       if(hasReturn==ballCount){
        ballController.currentBallState=BallController.ballState.endShot;
        
       }else
       {
        ballController.currentBallState=BallController.ballState.wait;
       }
    }
    public void EndTheShot(){
        
        ballCount+=plusCount;
        plusCount=0;
        hasReturn=0;
        Debug.Log("haReturn sifirlandi: "+hasReturn);
        ballController.currentBallState  = BallController.ballState.aim;
        
    }
}
