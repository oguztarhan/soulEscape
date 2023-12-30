using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BallManager : MonoBehaviour
{

    private Rigidbody topRigidbody;
    private Vector3 firlatmaYonu;
    private Vector3 firstPosition;

    [SerializeField, Range(0f, 250f)] private float speed;

    void Start()
    {
        topRigidbody = GetComponent<Rigidbody>();
        firstPosition=transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Firlat();
        }
    }

    void Firlat()
    {
        //topRigidbody.isKinematic = false;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            firlatmaYonu = (hit.point - transform.position).normalized;
            topRigidbody.velocity = firlatmaYonu * speed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 carpmaNoktasi = collision.contacts[0].point;
        firlatmaYonu = Vector3.Reflect(firlatmaYonu, collision.contacts[0].normal);
        topRigidbody.velocity = firlatmaYonu * speed;

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
        }else if(collision.gameObject.CompareTag("EndLine")){
            topRigidbody.velocity = Vector3.zero;
            topRigidbody.angularVelocity = Vector3.zero;
            //topRigidbody.isKinematic = true;
            transform.position=firstPosition;
            GridManager gridManager = FindObjectOfType<GridManager>();
        if (gridManager != null)
        {
            gridManager.ShiftPrefabsDown();
            gridManager.updateGrid();
        }
        }

    }
    
}
