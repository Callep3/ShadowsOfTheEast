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
