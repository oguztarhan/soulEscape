using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public enum ballState{
        aim,
        fire,
        wait,
        endShot
    }
    public ballState currentBallState;
    [SerializeField] Rigidbody ball;
    public int maxExtraBalls=10;
    public float timeBetweenExtraBalls = 0.5f;
    
    private Vector3 mouseStartPos;
    private Vector3 mouseEndPos;
    public Vector3 firstPosition;
    public GameObject extraBall;
  
    
    public int ballCount=10;
    
    private float ballVelocityX;
    private float ballVelocityY;
    
    [SerializeField] private float constantSpeed;
    [SerializeField]  private GameObject arrow;
    private Vector3 ballPos;
    private Quaternion ballRot;
    
    void Start()
    {
        ball = GetComponent<Rigidbody>();
        firstPosition = transform.position;
        currentBallState = ballState.aim;
        ballPos=transform.position;
        ballRot=transform.rotation;
        
        
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (currentBallState){
            case ballState.aim:
                if(Input.GetMouseButtonDown(0))
                {
                   MouseClicked();
                }
                if(Input.GetMouseButton(0))
                {
                    MouseDragged();
                }
                if(Input.GetMouseButtonUp(0))
                {
                    RelaseMouse();
                }
            break;
            case ballState.fire:
            break;
            case ballState.wait:
            break;
            case ballState.endShot:
            
            break;
            default:
            break;
        }
       
        //Rigidbody rb = GetComponent<Rigidbody>();
        
         /*if (rb.velocity.magnitude > constantSpeed)
    {
        rb.velocity = rb.velocity.normalized * constantSpeed;
    }*/
    }

    private void MouseDragged()
    {
        arrow.SetActive(true);
        Vector3 tempMousePos= Input.mousePosition;
        float diffX=mouseStartPos.x - tempMousePos.x;
        float diffY=mouseStartPos.y - tempMousePos.y;
        if(diffY<=0){
            diffY=.01f;
        }
        float theta = Mathf.Rad2Deg * Mathf.Atan(diffX/diffY);
        arrow.transform.rotation=Quaternion.Euler(0f,0f,-theta);
       
    
    }

    public void MouseClicked()
    {
        mouseStartPos=Input.mousePosition;
    }
    public void RelaseMouse()
    {
        arrow.SetActive(false);
          mouseEndPos=Input.mousePosition;
        ballVelocityX=(mouseStartPos.x-mouseEndPos.x);
        ballVelocityY=(mouseStartPos.y-mouseEndPos.y);
       // StopCoroutine("extraBalls");
      /*  Vector3 tempVelocity=new Vector3(ballVelocityX,ballVelocityY).normalized;
        ball.velocity=constantSpeed*tempVelocity;
        currentBallState=ballState.fire;*/
        
        Vector3 launchDirection = new Vector3(ballVelocityX, ballVelocityY).normalized;
        Vector3 finalVelocity = constantSpeed * launchDirection;
        ball.AddForce(finalVelocity, ForceMode.VelocityChange);

        currentBallState = ballState.fire;
       StartCoroutine(extraBalls(finalVelocity));
        
    }
     IEnumerator extraBalls(Vector3 initialVelocity)
    {
      for (int i = 0; i < ballCount - 1; i++)
        {
        yield return new WaitForSeconds(timeBetweenExtraBalls);
        GameObject extraBallInstance = Instantiate(extraBall, ballPos, ballRot);
        Rigidbody extraBallRigidbody = extraBallInstance.GetComponent<Rigidbody>();
        extraBallRigidbody.AddForce(initialVelocity, ForceMode.VelocityChange);
        }
        currentBallState=ballState.wait;
        
    }



    public void OnTriggerEnter(Collider other) {
        
            ballCount++;
            Destroy(other.gameObject);
    }
    public void OnCollisionEnter(Collision collision)
    {
        
      

        if (collision.gameObject.CompareTag("Tile"))
        {
            TextMeshPro textMesh = collision.gameObject.GetComponentInChildren<TextMeshPro>();
            if (textMesh != null && int.TryParse(textMesh.text, out int hitPoints))
            {
                hitPoints = Mathf.Max(0, hitPoints - 1);
                textMesh.text = hitPoints.ToString();
                if (hitPoints == 0)
                {
                    Destroy(collision.gameObject);
                }
            }
            else
            {
                Debug.LogError("hataaaaaaa");
            }
        }
        else if (collision.gameObject.CompareTag("EndLine"))
        {
            ball.velocity = new Vector3(0f,0f,0f);
            ball.angularVelocity = Vector3.zero;
           // transform.position = firstPosition;
           ballPos=transform.position;
           ballRot=transform.rotation;
            GridManager gridManager = FindObjectOfType<GridManager>();
           
            if (gridManager != null)
            {
                gridManager.ShiftPrefabsDown();
                gridManager.updateGrid();
            }
           
        }
        
    }
   /* public void DestroyExtraBalls(){
        Destroy (GameObject.FindWithTag("ExtraBall"));
    }*/
   
}
