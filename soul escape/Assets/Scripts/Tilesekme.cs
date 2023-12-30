using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilesekme : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball")) // Eğer çarpan nesne "Top" tag'ına sahipse
        {
            Rigidbody topRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            
            if (topRigidbody != null)
            {
                // Çarptığı nesnenin normal vektörünü kullanarak sekme yönünü belirle
                Vector3 sekmeYonu = collision.contacts[0].normal;

                // Çarpmadan gelen normal vektörle çarpım yaparak sekme hızını belirle
                float sekmeHizi = 5f; // İstediğiniz hızı ayarlayabilirsiniz
                topRigidbody.velocity = sekmeYonu * sekmeHizi;
            }
        }
    }
}
