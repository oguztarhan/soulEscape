using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TileHealthManager : MonoBehaviour
{   
    public int tileHP;
    private TextMeshPro tileHPtext;
    private GridManager gridManager;
    void Start()
    {
        gridManager = FindAnyObjectByType<GridManager>();
        tileHP=gridManager.level;
        tileHPtext = GetComponentInChildren<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        tileHPtext.text=""+tileHP;
        if (tileHP<=0){
            Destroy(gameObject);
        }
    }
    void TakeDamage(int damagetoTake)
    {
        tileHP-=damagetoTake;
    }
     void OnCollisionExit(Collision other) {
        if(other.gameObject.tag=="Ball"){
            TakeDamage(1);
        }
        else if(other.gameObject.tag=="ExtraBall"){
            TakeDamage(1);
        }
    }
}
