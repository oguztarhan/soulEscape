using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Material _baseColor, _offsetColor;
    [SerializeField] private MeshRenderer _renderer;

    public void Init(bool isOffset){
      //  _renderer.material= isOffset ? _offsetColor:_baseColor;
    }
}
