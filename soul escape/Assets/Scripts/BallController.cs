using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] Rigidbody ball;
    
    private Vector3 mouseStartPos;
    private Vector3 mouseEndPos;
    public Vector3 firstPosition;
    public bool didClick;
    public bool didDrag;
    public int ballCount;
    public bool canInteract;
    private float ballVelocityX;
    private float ballVelocityY;
    [SerializeField] private float constantSpeed;
    [SerializeField]  private GameObject arrow;
    void Start()
    {
        ball = GetComponent<Rigidbody>();
        firstPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if(Input.GetMouseButtonDown(0)&&canInteract)
        {
            MouseClicked();
        }
        if(Input.GetMouseButton(0)&&canInteract)
        {
            MouseDragged();
        }
        if(Input.GetMouseButtonUp(0)&&canInteract)
        {
            RelaseMouse();
        }
         if (rb.velocity.magnitude > constantSpeed)
    {
        rb.velocity = rb.velocity.normalized * constantSpeed;
    }
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
       didDrag=true;
       Debug.Log("dragged");
    }

    private void MouseClicked()
    {
        mouseStartPos=Input.mousePosition;
        didClick=true;
        Debug.Log(mouseStartPos);
    }
    public void RelaseMouse()
    {
        arrow.SetActive(false);
        mouseEndPos=Input.mousePosition;
        ballVelocityX=(mouseStartPos.x-mouseEndPos.x);
        ballVelocityY=(mouseStartPos.y-mouseEndPos.y);
        Vector3 tempVelocity=new Vector3(ballVelocityX,ballVelocityY).normalized;
        ball.velocity=constantSpeed*tempVelocity;
        didClick=false;
        didDrag=false;
        canInteract=false;
        Debug.Log(ball.velocity);
        Debug.Log(mouseEndPos);
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
            ball.velocity = Vector3.zero;
            ball.angularVelocity = Vector3.zero;
            transform.position = firstPosition;
            GridManager gridManager = FindObjectOfType<GridManager>();
            ballCount++;
            if (gridManager != null)
            {
                gridManager.ShiftPrefabsDown();
                gridManager.updateGrid();
            } 
            canInteract=true;

            
        }
        else if (collision.gameObject.CompareTag("Plus"))
        {
            ballCount++;
            Destroy(collision.gameObject);
        }
    }
}
