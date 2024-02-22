using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundAudioManager : MonoBehaviour
{
    public static BackgroundAudioManager instance;

    [SerializeField] AudioSource backgroundEffSource;
    [SerializeField] AudioClip staticNoise;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    public void PlayStaticNoise()
    {
        backgroundEffSource.loop = true;
        backgroundEffSource.clip = staticNoise;
        backgroundEffSource.Play();
    }
    public void StopStaticNoise()
    {
        backgroundEffSource.loop = false;
        backgroundEffSource.Stop();
    }
}
