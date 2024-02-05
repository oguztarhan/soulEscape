using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TileHealthManager : MonoBehaviour
{
    public int tileHP;
    private TextMeshPro tileHPtext;
    private GridManager gridManager;
    public ParticleSystem explosion;
    Vector3 position;
    AudioSource audioSource;

    void Start()
    {
        gridManager = FindAnyObjectByType<GridManager>();
        tileHP = gridManager.level;
        tileHPtext = GetComponentInChildren<TextMeshPro>();
        ParticleSystem ps = GetComponent<ParticleSystem>();
        ParticleSystem.MainModule ma = ps.main;
        audioSource = GetComponent<AudioSource>();
       
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;
        tileHPtext.text = "" + tileHP;
        if (tileHP <= 0)
        {
            Explode();
            Destroy(gameObject);
        }
    }
    void Explode()
    {

        Instantiate(explosion, position, Quaternion.identity);
        explosion.Play();
    }
    void TakeDamage(int damagetoTake)
    {
        tileHP -= damagetoTake;
        audioSource.Play();

    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ball")
        {
            TakeDamage(1);
            
        }
        else if (other.gameObject.tag == "ExtraBall")
        {
            TakeDamage(1);

        }
    }
}
