using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Azrail : MonoBehaviour
{
    public GameObject top;
    Vector2 firstPosition;
    Vector2 sonPos;
    bool isDrawing = false;
    LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = top.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            top.SetActive(true);
            isDrawing = true;
            lineRenderer.enabled = true;
        }

        if (isDrawing && Input.GetMouseButton(0))
        {
            sonPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            float angle = Mathf.Atan2(firstPosition.y - sonPos.y, firstPosition.x - sonPos.x) * Mathf.Rad2Deg;
            top.transform.rotation = Quaternion.Euler(angle, angle, 0);
             lineRenderer.SetPosition(0, firstPosition);
            lineRenderer.SetPosition(1, sonPos);

            // Çizgi çizimi
           /* Debug.DrawLine(Camera.main.ScreenToWorldPoint(new Vector3(firstPosition.x * Screen.width, firstPosition.y * Screen.height, 10)),
                           Camera.main.ScreenToWorldPoint(new Vector3(sonPos.x * Screen.width, sonPos.y * Screen.height, 10)),
                           Color.red);*/
        }

        if (Input.GetMouseButtonUp(0))
        {
            //top.SetActive(false);
            isDrawing = false;
            lineRenderer.enabled = false;
        }
    }
}