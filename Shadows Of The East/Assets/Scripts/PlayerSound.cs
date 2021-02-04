using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerSound : MonoBehaviour
{
    [SerializeField] private AudioClip gettingHitSound;
    [SerializeField] private AudioClip lightAttackSound;
    [SerializeField] private AudioClip heavyAttackSound;
    [SerializeField] private AudioClip dyingSound;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnGettingHit()
    {
        if (audioSource.isPlaying && audioSource.clip != dyingSound)
        {
            audioSource.clip = gettingHitSound;
            audioSource.Play();
        }
        else if (!audioSource.isPlaying)
        {
            audioSource.clip = gettingHitSound;
            audioSource.Play();
        }
    }

    public void OnDeath()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
            audioSource.clip = dyingSound;
            audioSource.Play();
        }
        else if (!audioSource.isPlaying)
        {
            audioSource.clip = dyingSound;
            audioSource.Play();
        }
    }

    public void OnLightAttack()
    {
        audioSource.clip = lightAttackSound;
        audioSource.PlayOneShot(lightAttackSound);
    }

    public void OnHeavyAttack()
    {
        audioSource.clip = heavyAttackSound;
        audioSource.PlayOneShot(heavyAttackSound);
    }
}
