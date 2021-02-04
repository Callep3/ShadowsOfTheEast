using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusic;
    [Header("Moan settings")]
    [SerializeField] private int maxAmountOfMoans = 5;
    private List<AudioSource> moanSources = new List<AudioSource>();
    [Header("Zombie getting hit setting")]
    [SerializeField] private int maxAmountOfHitSettings = 5;
    private List<AudioSource> gettingHitSources = new List<AudioSource>();
    [Header("Zombie dying sound settings")]
    [SerializeField] private int maxAmountOfDyingSounds = 5;
    private List<AudioSource> dyingSources = new List<AudioSource>();
    [Header("Zombie attack sound settings")]
    [SerializeField] private int maxAmountOfAttackSounds = 5;
    private List<AudioSource> attackSources = new List<AudioSource>();

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
    #region Getting hit sounds 
    public bool AllowedToPlayGettingHitSound(AudioSource hitSound)
    {
        UpdateGettingHitSound();
        if (gettingHitSources.Count >= maxAmountOfHitSettings)
        {
            return false;
        }
        else
        {
            gettingHitSources.Add(hitSound);
            return true;
        }
    }

    private void UpdateGettingHitSound()
    {
        List<AudioSource> toRemoveSources = new List<AudioSource>();
        foreach (AudioSource source in gettingHitSources)
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
            gettingHitSources.Remove(source);
        }
    }
    #endregion

    #region Moaning sounds
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
    #endregion

    #region Dying sounds
    public bool AllowedToPlayDyingSound(AudioSource source)
    {
        UpdateDyingSounds();
        if (dyingSources.Count >= maxAmountOfDyingSounds)
        {
            return false;
        }
        else
        {
            dyingSources.Add(source);
            return true;
        }
    }

    private void UpdateDyingSounds()
    {
        List<AudioSource> toRemoveSources = new List<AudioSource>();
        foreach (AudioSource source in dyingSources)
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
            dyingSources.Remove(source);
        }
    }
    #endregion

    #region Zombie attack sounds
    public bool AllowedToPlayAttackSound(AudioSource source)
    {
        UpdateAttackSounds();
        if (attackSources.Count >= maxAmountOfAttackSounds)
        {
            return false;
        }
        else
        {
            attackSources.Add(source);
            return true;
        }
    }

    private void UpdateAttackSounds()
    {
        List<AudioSource> toRemoveSources = new List<AudioSource>();
        foreach (AudioSource source in attackSources)
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
            attackSources.Remove(source);
        }
    }
    #endregion
}
