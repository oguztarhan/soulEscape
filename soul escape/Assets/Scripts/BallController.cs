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
    public Rigidbody ball;
    public float timeBetweenExtraBalls = 0.1f;
    private Vector3 mouseStartPos;
    private Vector3 mouseEndPos;
    public Vector3 firstPosition;
    public GameObject extraBall;
    private float ballVelocityX;
    private float ballVelocityY;
    [SerializeField] private float constantSpeed;
    [SerializeField]  private GameObject arrow;
    public Vector3 ballPos;
    public Quaternion ballRot;
    GridManager gridManager;
    FloorCollider floorCollider;
    int ballCounting;
    void Start()
    {
        ball = GetComponent<Rigidbody>();
        firstPosition = transform.position;
        currentBallState = ballState.aim;
        ballPos=transform.position;
        ballRot=transform.rotation;
        gridManager = FindObjectOfType<GridManager>();
        floorCollider = FindAnyObjectByType<FloorCollider>();
        
        
        
        
    }
    void Update()
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
            floorCollider.WaitForTheShot();
            break;
            case ballState.endShot:
            floorCollider.EndTheShot();
            break;
            default:
            break;
        }
        ballCounting=floorCollider.ballCount;
        Debug.Log("currentstate:"+currentBallState);
       // Debug.Log("ballCounting:"+ballCounting);
       // Debug.Log("hasreturn:"+floorCollider.hasReturn);
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
        Vector3 launchDirection = new Vector3(ballVelocityX, ballVelocityY).normalized;
        Vector3 finalVelocity = constantSpeed * launchDirection;
        ball.AddForce(finalVelocity, ForceMode.VelocityChange);
        StartCoroutine(extraBalls(finalVelocity));
        //currentBallState = ballState.fire;
       // Debug.Log("relased");
    }
     IEnumerator extraBalls(Vector3 initialVelocity)
    {
        //Debug.Log("enumator");
      for (int i = 0; i <ballCounting-1; i++)
        {
        yield return new WaitForSeconds(timeBetweenExtraBalls);
        GameObject extraBallInstance = Instantiate(extraBall, ballPos, ballRot);
        Rigidbody extraBallRigidbody = extraBallInstance.GetComponent<Rigidbody>();
        extraBallRigidbody.AddForce(initialVelocity, ForceMode.VelocityChange);
        //Debug.Log("girdi");
       // Debug.Log(i);
        }
        
        currentBallState=ballState.wait;
        
    }
    public void OnTriggerEnter(Collider other) 
    {
           if(other.gameObject.CompareTag("Plus")) 
            {
                floorCollider.plusCount++;
                Destroy(other.gameObject);
                Debug.Log("yokedildi");
            }
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
        }else if (collision.gameObject.CompareTag("EndLine"))
        {
            //ballController = collision.gameObject.GetComponent<BallController>();
            ball.velocity = new Vector3(0f,0f,0f);
            ball.angularVelocity = Vector3.zero;
            ballPos=transform.position;
            ballRot=transform.rotation;
            floorCollider.hasReturn++;
            Debug.Log("hasReturn sayısı: " +floorCollider.hasReturn);
        }
        
    }
   
}
