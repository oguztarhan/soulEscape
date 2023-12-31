using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    private Rigidbody topRigidbody;
    private Vector3 firlatmaYonu;
    private Vector3 firstPosition;
    private Vector3 mouseDownPosition;
    private Vector3 mouseUpPosition;

    [SerializeField, Range(0f, 250f)] private float speed;
    

    void Start()
    {
        topRigidbody = GetComponent<Rigidbody>();
        firstPosition = transform.position;
    }

    void Update()
    {
       if (Input.GetMouseButtonDown(0))
        {
            OnMouseDown();
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnMouseUp();
        }
    }

    void OnMouseDown()
    {
        mouseDownPosition = Input.mousePosition;
    }

    void OnMouseUp()
    {
        mouseUpPosition = Input.mousePosition;
        Firlat();
    }

    void Firlat()
    {
        // Topun týklanýldýðý ve býrakýldýðý noktalar arasýndaki farký hesapla
        Vector3 fark = mouseUpPosition - mouseDownPosition;

        // Topun týklanýldýðý ve býrakýldýðý yöne gitmesini saðla
        firlatmaYonu = new Vector3(fark.x, fark.y, 0).normalized;

        // Topun hýzýný ayarla ve hareketini baþlat
        topRigidbody.velocity = firlatmaYonu * speed;
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
        }
        else if (collision.gameObject.CompareTag("EndLine"))
        {
            topRigidbody.velocity = Vector3.zero;
            topRigidbody.angularVelocity = Vector3.zero;
            transform.position = firstPosition;
            GridManager gridManager = FindObjectOfType<GridManager>();
            if (gridManager != null)
            {
                gridManager.ShiftPrefabsDown();
                gridManager.updateGrid();
            }
        }
    }
}