using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : MonoBehaviour
{
    [SerializeField] private float minimumTimeBeforeMoan = 3f;
    [SerializeField] private float maximumTimeBeforeMoan = 10f;
    [SerializeField] private float timerUntilNextMoan;
    [SerializeField] private AudioClip moanClip;
    [SerializeField] private AudioClip gettingHitSound;
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip dyingSound;
    AudioSource AudioSource;
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        timerUntilNextMoan = Random.Range(minimumTimeBeforeMoan, maximumTimeBeforeMoan);
    }

    public void GotHit()
    {
        if (!AudioSource.isPlaying)
        {
            if (SoundManager.Instance.AllowedToPlayGettingHitSound(AudioSource))
            {
                AudioSource.clip = gettingHitSound;
                AudioSource.Play();
            }
        }
    }

    public void Died()
    {
        if (!AudioSource.isPlaying)
        {
            if (SoundManager.Instance.AllowedToPlayDyingSound(AudioSource))
            {
                AudioSource.clip = dyingSound;
                AudioSource.Play();
            }
        }
    }

    public void Attacked()
    {
        if (!AudioSource.isPlaying)
        {
            if (SoundManager.Instance.AllowedToPlayAttackSound(AudioSource))
            {
                AudioSource.clip = attackSound;
                AudioSource.Play();
            }
        }
    }

    void Update()
    {
        timerUntilNextMoan -= Time.deltaTime;
        if (timerUntilNextMoan <= 0 && !AudioSource.isPlaying)
        {
            if (SoundManager.Instance.AllowedToPlayMoan(AudioSource))
            {
                AudioSource.clip = moanClip;
                AudioSource.Play();
            }            
            timerUntilNextMoan = Random.Range(minimumTimeBeforeMoan, maximumTimeBeforeMoan);
        }
    }
}
