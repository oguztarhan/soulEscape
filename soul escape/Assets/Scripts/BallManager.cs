using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{

    public float flingForce = 10f; // Fırlatma kuvveti
    private GameObject _ballPrefab;
    
    void Update()
    {
        // Mouse sol tıklama algıla
        if (Input.GetMouseButtonDown(0))
        {
            // Fare pozisyonunu al
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f; // Top düzlemde olduğu için z ekseni sıfır olmalı

            // Topun mevcut pozisyonunu al
            Vector3 ballPosition = transform.position;

            // Fare pozisyonu ve top pozisyonu arasındaki yönü hesapla
            Vector3 flingDirection = (mousePosition - ballPosition).normalized;

            // Fırlatma kuvvetini uygula
            GetComponent<Rigidbody>().AddForce(flingDirection * flingForce, ForceMode.Impulse);
        }
    }
    int ballCount =1;
    private void OnCollisionEnter(Collision other) {
            Debug.Log("Collusion happened!");
        if(other.gameObject.CompareTag("Plus")){


            Destroy(other.gameObject);
            ballCount++;
        }
    }
}
