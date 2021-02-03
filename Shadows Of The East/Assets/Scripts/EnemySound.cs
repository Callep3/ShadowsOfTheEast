using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : MonoBehaviour
{
    [SerializeField] private float minimumTimeBeforeMoan = 3f;
    [SerializeField] private float maximumTimeBeforeMoan = 10f;
    [SerializeField] private float timerUntilNextMoan;
    AudioSource AudioSource;
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        timerUntilNextMoan = Random.Range(minimumTimeBeforeMoan, maximumTimeBeforeMoan);
    }

    // Update is called once per frame
    void Update()
    {
        timerUntilNextMoan -= Time.deltaTime;
        if (timerUntilNextMoan <= 0 )
        {
            if (SoundManager.Instance.AllowedToPlayMoan(AudioSource))
            {
                AudioSource.Play();
                timerUntilNextMoan = Random.Range(minimumTimeBeforeMoan, maximumTimeBeforeMoan);
            }            
        }
    }
}
