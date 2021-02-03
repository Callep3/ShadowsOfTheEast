using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private int maxAmountOfMoans = 5;
    [SerializeField] List<AudioSource> moanSources = new List<AudioSource>();
    public static SoundManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }



    public void StartBackgroundMusic()
    {
        backgroundMusic.Play();
    }

    public void StopBackgroundMusic()
    {
        backgroundMusic.Stop();
    }
    public bool AllowedToPlayMoan(AudioSource moan)
    {
        UpdateMoans();
        if (moanSources.Count >= maxAmountOfMoans)
        {
            return false;
        }
        else
        {
            moanSources.Add(moan);
            return true;
        }
    }

    private void UpdateMoans()
    {
        List<AudioSource> toRemoveSources = new List<AudioSource>();
        foreach (AudioSource source in moanSources)
        {
            if (source != null)
            {
                if (!source.isPlaying)
                {
                    toRemoveSources.Add(source);
                }
            }
            else
            {
                toRemoveSources.Add(source);
            }
        }

        foreach (AudioSource source in toRemoveSources)
        {
            moanSources.Remove(source);
        }
    }

}
