using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer instance;

    // Singleton pattern
    public static MusicPlayer Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MusicPlayer>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("MusicPlayer");
                    instance = singletonObject.AddComponent<MusicPlayer>();
                }
            }

            return instance;
        }
    }

    // Müzik çaların özellikleri
    private AudioSource audioSource;
     private AudioClip currentMusicClip;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // AudioSource bileşenini ekleyin
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = true;
            //RestartMusic();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Müziği çal
    public void PlayMusic(AudioClip musicClip)
    {
        audioSource.clip = musicClip;
        audioSource.Play();
        if (musicClip != null)
        {
            currentMusicClip = musicClip;
            audioSource.clip = currentMusicClip;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("Music clip is null. Please provide a valid audio clip.");
        }
    }

    // Müziği durdur
    public void StopMusic()
    {
        audioSource.Stop();
    }

    // Müziği duraklat
    public void PauseMusic()
    {
        audioSource.Pause();
    }

    // Müziği devam ettir
    public void ResumeMusic()
    {
        audioSource.UnPause();
    }
    public void RestartMusic()
    {
        if (currentMusicClip != null)
        {
            audioSource.Stop();
            audioSource.clip = currentMusicClip;
            audioSource.Play();
        }
    }
}
