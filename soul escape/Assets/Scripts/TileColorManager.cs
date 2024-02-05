using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileColorManager : MonoBehaviour
{
    public Gradient gradient;
    private new Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.material.color = gradient.Evaluate(Random.Range(0f,1f));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
