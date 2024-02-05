using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraBallColllisions : MonoBehaviour
{
    BallController ballController;
    public int hasReturn = 0;
    Vector3 lastVelocity;

Rigidbody rb;
    void Start()
    {
        ballController = FindAnyObjectByType<BallController>();
         rb = GetComponent<Rigidbody>();
    }
    private void Update() {
        
        lastVelocity=rb.velocity;
    }
    public void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.CompareTag("EndLine"))
        {
            GameManager.Instance.AddHasReturn();
            Destroy(gameObject);
        }else{
             var speed=lastVelocity.magnitude;
            var direction=Vector3.Reflect(lastVelocity.normalized,other.contacts[0].normal);
            rb.velocity= direction*Mathf.Max(speed,0f);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Plus"))
        {
            ballController.plusCount++;
            Destroy(other.gameObject);
        }
    }


}
