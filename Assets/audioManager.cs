using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] AudioSource soundEffSource;

    [SerializeField] AudioClip pickUpNoise;
    [SerializeField] AudioClip newInstructionsNoise;
    [SerializeField] AudioClip talkingNoise;
    [SerializeField] AudioClip openJournalNoise;
    [SerializeField] AudioClip openBagNoise;
    // Start is called before the first frame update
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
    public void PlayTalkingNoise()
    {
        soundEffSource.loop = true;
        soundEffSource.clip = talkingNoise;
        soundEffSource.Play();
    }
    public void StopTalkingNoise()
    {
        soundEffSource.loop = false;
        soundEffSource.Stop();
    }
    public void PlayPickUpNoise()
    {
        soundEffSource.loop = false;
        soundEffSource.clip = pickUpNoise;
        soundEffSource.Play();
    }
    public void PlayNewInstuctionsNoise()
    {
        soundEffSource.loop = false;
        soundEffSource.clip = newInstructionsNoise;
        soundEffSource.Play();
    }

    public void PlayOpenJournal()
    {
        soundEffSource.loop = false;
        soundEffSource.clip = openJournalNoise;
        soundEffSource.Play();
    }
    public void PlayOpenInventory()
    {
        soundEffSource.loop = false;
        soundEffSource.clip = openBagNoise;
        soundEffSource.Play();
    }   

}
