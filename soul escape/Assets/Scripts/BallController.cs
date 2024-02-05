using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class BallController : MonoBehaviour
{
    private static BallController instance;
    public static BallController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BallController>(true);
            }

            return instance;
        }
    }
     AudioSource audioSource;
    public enum ballState
    {
        aim,
        fire,
        wait,
        endShot
    }
    Vector3 lastVelocity;
    public ballState currentBallState;
    [SerializeField] Rigidbody ball;
    public int maxExtraBalls = 10;
    public float timeBetweenExtraBalls = 0.5f;
   
public Vector3 instantiatePosition;
    private Vector3 mouseStartPos;
    private Vector3 mouseEndPos;
    public Vector3 firstPosition;
    public GameObject extraBall;
    public int ballCount = 10;
    private float ballVelocityX;
    private float ballVelocityY;
    [SerializeField] public float constantSpeed;
    [SerializeField] private GameObject arrow;
    private Vector3 ballPos;
    private Vector3 newBallPos;
    private Quaternion ballRot;
    public int TimeOutS;
    GridManager gridManager;
    public int plusCount;
    public float targetxPos;
    ExtraBallColllisions extraBallColllisions;
    playerControls playercontrols;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        ball = GetComponent<Rigidbody>();
        firstPosition = transform.position;
        ChangeBallState(ballState.aim);
        ballPos = transform.position;
        ballRot = transform.rotation;
        gridManager = FindObjectOfType<GridManager>();
        extraBallColllisions = FindAnyObjectByType<ExtraBallColllisions>();
        
        
        playercontrols = FindAnyObjectByType<playerControls>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentBallState)
        {
            case ballState.aim:
                if (Input.GetMouseButtonDown(0))
                {
                    MouseClicked();
                }
                if (Input.GetMouseButton(0))
                {
                    MouseDragged();
                }
                if (Input.GetMouseButtonUp(0))
                {
                    RelaseMouse();
                }
                break;
            case ballState.fire:

                break;
            case ballState.wait:

                break;
            case ballState.endShot:
                endTheShot();


                break;
            default:
                break;
        }
        lastVelocity = ball.velocity;
        targetxPos=transform.position.x;
    }

    private void MouseDragged()
    {
        arrow.SetActive(true);
        Vector3 tempMousePos = Input.mousePosition;
        float diffX = mouseStartPos.x - tempMousePos.x;
        float diffY = mouseStartPos.y - tempMousePos.y;
        if (diffY <= 0)
        {
            diffY = .01f;
        }
        float theta = Mathf.Rad2Deg * Mathf.Atan(diffX / diffY);
        arrow.transform.rotation = Quaternion.Euler(0f, 0f, -theta);


    }

    public void ChangeBallState(ballState ballState)
    {
        currentBallState = ballState;
        switch (currentBallState)
        {
            case ballState.aim:
               // Debug.Log("Aim");
                break;
            case ballState.fire:
                break;
            case ballState.wait:
             //   Debug.Log("Wait");
                break;
            case ballState.endShot:
              //  Debug.Log("End Shot");
                break;
        }
    }

    public void MouseClicked()
    {
        mouseStartPos = Input.mousePosition;
    }
    public void RelaseMouse()
    {
        arrow.SetActive(false);
        mouseEndPos = Input.mousePosition;
        ballVelocityX = (mouseStartPos.x - mouseEndPos.x);
        ballVelocityY = (mouseStartPos.y - mouseEndPos.y);
        
        Vector3 launchDirection = new Vector3(ballVelocityX, ballVelocityY).normalized;
        if(launchDirection==Vector3.zero)return;
        Vector3 finalVelocity = constantSpeed * launchDirection;
        ball.AddForce(finalVelocity, ForceMode.VelocityChange);
        ChangeBallState(ballState.fire);
        StartCoroutine(extraBalls(finalVelocity));

    }
    IEnumerator extraBalls(Vector3 initialVelocity)
    {
        for (int i = 0; i < ballCount - 1; i++)
        {
            yield return new WaitForSeconds(timeBetweenExtraBalls);
            instantiatePosition = new Vector3(newBallPos.x, newBallPos.y, newBallPos.z);
            GameObject extraBallInstance = Instantiate(extraBall, instantiatePosition, ballRot);
            Rigidbody extraBallRigidbody = extraBallInstance.GetComponent<Rigidbody>();
            extraBallRigidbody.AddForce(initialVelocity, ForceMode.VelocityChange);
            ChangeBallState(ballState.wait);
        }
        
    }
    public void OnTriggerEnter(Collider other)
    {

        plusCount++;
        Destroy(other.gameObject);
    }
    public void OnCollisionEnter(Collision collision)
    {
            
        if (collision.gameObject.CompareTag("EndLine"))
        {
            ball.velocity = new Vector3(0f, 0f, 0f);
            ball.angularVelocity = Vector3.zero;
            ballPos = transform.position;
            ballRot = transform.rotation;
            targetxPos=transform.position.x;
            ballPos.x=Mathf.Clamp(ballPos.x,-0.075f,4.2f);
            newBallPos=new Vector3(ballPos.x,ballPos.y+0.3f,ballPos.z);
            ball.transform.position=newBallPos;
            //GameManager.Instance.AddHasReturn();
        }
        else{
            var speed = lastVelocity.magnitude;
                var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
                ball.velocity = direction * Mathf.Max(speed, 0f);
        }
        
    }
    public void endTheShot()
    {
        ballCount+=plusCount;
        plusCount=0;
        ChangeBallState(ballState.aim);
        playercontrols.moveTheFCharacter();
        gridManager.ShiftPrefabsDown();
        gridManager.updateGrid();

    }
}
