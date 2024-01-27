using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Xml;

public class FloorCollider : MonoBehaviour
{
    private int destroyingBalls;
    public int results;
    
    public void Start()
    {
        BallController ballController = FindAnyObjectByType<BallController>();
        destroyingBalls=ballController.ballCount;
        //Debug.Log(destroyingBalls);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag=="ExtraBall")
        {
            destroyingBalls--;
            Destroy(collision.gameObject);
            //collision.gameObject.GetComponent<Rigidbody>().velocity=Vector3.zero;
            
            
        }
        Debug.Log(destroyingBalls);
        results=destroyingBalls;
    }
}
