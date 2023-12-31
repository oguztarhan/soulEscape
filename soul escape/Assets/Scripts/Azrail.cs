using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Azrail : MonoBehaviour
{
    public GameObject top;
    Vector2 baslangýçPos;
    Vector2 sonPos;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            baslangýçPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            top.SetActive(true);
        }

        if (Input.GetMouseButton(0))
        {
            sonPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            float angle = Mathf.Atan2(baslangýçPos.y - sonPos.y, baslangýçPos.x - sonPos.x) * Mathf.Rad2Deg;
            top.transform.rotation = Quaternion.Euler(angle, angle,0);
        }

        if (Input.GetMouseButtonUp(0))
        {
            top.SetActive(false);
        }

        // Çizgi çizimi için Debug.DrawLine kullanýmý
        Debug.DrawLine(Camera.main.ScreenToWorldPoint(new Vector3(baslangýçPos.x * Screen.width, baslangýçPos.y * Screen.height, 10)),
                       Camera.main.ScreenToWorldPoint(new Vector3(sonPos.x * Screen.width, sonPos.y * Screen.height, 10)),
                       Color.red);
    }
}
