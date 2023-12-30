using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{

    private Rigidbody topRigidbody;
    private Vector3 firlatmaYonu;

    [SerializeField, Range(0f, 250f)] private float speed;

    void Start()
    {
        topRigidbody = GetComponent<Rigidbody>();
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
    }
}
