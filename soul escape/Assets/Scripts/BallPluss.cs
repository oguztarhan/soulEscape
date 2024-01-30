using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPluss : MonoBehaviour
{
    // Start is called before the first frame update
    FloorCollider floorCollider ;
    void Start()
    {
         floorCollider = FindAnyObjectByType<FloorCollider>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other) {
        
        if(other.gameObject.CompareTag("Plus")){
            floorCollider.ballCount++;
            Destroy(other.gameObject);
        }
            
    }
    public void OnCollisionEnter(Collision other){
        
        if (other.gameObject.CompareTag("EndLine"))
        {
            
            Debug.Log("yokettim!!!");
            floorCollider.hasReturn++;
            Destroy(gameObject);
            Debug.Log(floorCollider.hasReturn);
        }
    }
     
        
        
       
    }

