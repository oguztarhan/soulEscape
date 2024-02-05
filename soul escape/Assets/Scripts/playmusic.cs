using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playmusic : MonoBehaviour
{
   public AudioClip yourMusicClip;

    void Start()
    {
        MusicPlayer.Instance.PlayMusic(yourMusicClip);
    }
}
