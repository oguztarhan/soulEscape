using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopKontrol : MonoBehaviour
{
    private Rigidbody topRigidbody;
    private Vector3 baslangicPozisyon;
    private bool firlatmaModu = false;
    private Vector3 firlatmaYonu;

    private float _lastMousePositionX, _lastMousePositionY, _deltaX, _deltaY;

    public LayerMask ballLayerMask;
    public LayerMask wallLayerMask;

    void Start()
    {
        topRigidbody = GetComponent<Rigidbody>();
        baslangicPozisyon = transform.position;
        topRigidbody.isKinematic = true;
    }

    void Update()
    {
        if (!firlatmaModu && Input.GetMouseButtonDown(0))
        {
            _lastMousePositionX = Input.mousePosition.x;
            _lastMousePositionY = Input.mousePosition.y;

            RaycastAta();
        }

        if (firlatmaModu)
        {
            HareketiKontrolEt();
        }
    }

    void RaycastAta()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.Log("aldi");
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ballLayerMask))
        {
            baslangicPozisyon = transform.position;

            firlatmaModu = true;
            topRigidbody.isKinematic = false;
        }
    }

    void HareketiKontrolEt()
    {
        if (Input.GetMouseButtonUp(0))
        {
            _deltaX = Input.mousePosition.x - _lastMousePositionX;
            _deltaY = Input.mousePosition.y - _lastMousePositionY;

            Firlat();
        }
    }

    void Firlat()
    {
        // Topu f�rlatma y�n�ne ayarla
        firlatmaYonu = new Vector3(-_deltaX, -_deltaY, 0f).normalized;

        // F�rlatma i�lemi
        firlatmaModu = false;
        topRigidbody.velocity = firlatmaYonu.normalized * speed;
        topRigidbody.angularVelocity = Vector3.zero;
    }

    void TopuSifirla()
    {
        topRigidbody.velocity = Vector3.zero;
        topRigidbody.angularVelocity = Vector3.zero;
        topRigidbody.isKinematic = true;
        transform.position = baslangicPozisyon;
    }

    [SerializeField, Range(0f, 250f)] private float speed;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7 )
        {
            WallScript wallScript = collision.gameObject.GetComponent<WallScript>();
            if (wallScript != null)
            {
                firlatmaYonu = Vector3.Reflect(firlatmaYonu, wallScript.normal);

                topRigidbody.velocity = firlatmaYonu.normalized * speed;
                topRigidbody.angularVelocity = Vector3.zero;
            }
        }
        else if (collision.gameObject.CompareTag("Tile"))
        {
            WallScript wallScript = collision.gameObject.GetComponent<WallScript>();
            if (wallScript != null)
            {
                firlatmaYonu = Vector3.Reflect(firlatmaYonu, wallScript.normal);

                topRigidbody.velocity = firlatmaYonu.normalized * speed;
                topRigidbody.angularVelocity = Vector3.zero;
            }
        }
        else if (collision.gameObject.layer == 8)
        {
            FloorWall wallScript = collision.gameObject.GetComponent<FloorWall>();
            if (wallScript != null)
            {
                topRigidbody.velocity = Vector3.zero;
                topRigidbody.angularVelocity = Vector3.zero;
                topRigidbody.isKinematic = true;
            }
        }
    }
}
